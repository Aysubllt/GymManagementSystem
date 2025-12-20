using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class AIController : Controller
{
    private readonly GroqApiService _aiService = new GroqApiService();

    // Dependency Injection kullanarak servisi alıyoruz
    public AIController()
    {
        _aiService = new GroqApiService();
    }

    public IActionResult Index() => View(new AIRecommendationViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetRecommendation(AIRecommendationViewModel model)
    {
        if (model.Weight == null || model.Height == null || model.Age == null)
        {
            TempData["Error"] = "Lütfen tüm vücut bilgilerini eksiksiz doldurun.";
            return View("Index", model);
        }

        // Profesyonel Prompt Mühendisliği
        string prompt = $@"
            Sen SAU GYM spor salonunda çalışan profesyonel bir fitness koçu ve beslenme uzmanısın. 
            Müşterinin bilgileri şunlar: 
            Yaş: {model.Age}, Cinsiyet: {model.Gender}, Boy: {model.Height}cm, Kilo: {model.Weight}kg, 
            Vücut Tipi: {model.BodyType}, Hedef: {model.Goal}.

            Lütfen şu formatta bir yanıt oluştur:
            1. **Vücut Analizi:** Kullanıcının verilerine göre kısa bir değerlendirme.
            2. **Haftalık Antrenman Planı:** (Örn: Pazartesi-Çarşamba-Cuma için temel egzersizler).
            3. **Beslenme Stratejisi:** Hedefine uygun temel makro önerileri (kalori hesabı yapma, genel öneri ver).
            4. **Motivasyon Sözü:** Kısa ve etkileyici bir bitiş.

            Önemli: Yanıtı Markdown formatında ver, başlıkları kalın yap. Samimi ama profesyonel bir dil kullan.";

        model.Result = await _aiService.GetGymRecommendationAsync(prompt);

        return View("Index", model);
    }
}