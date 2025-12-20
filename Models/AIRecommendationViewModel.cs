namespace GymManagementSystem.Models
{
    public class AIRecommendationViewModel
    {
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }

        // Kilo verme, Kas yapma, Kondisyon, Esneklik
        public string? Goal { get; set; }

        // Ektomorf (Zayıf), Mezomorf (Atletik), Endomorf (Geniş)
        public string? BodyType { get; set; }

        // Günlük Aktivite Seviyesi: Az, Orta, Çok
        public string? ActivityLevel { get; set; }

        public string? Result { get; set; } // Gemini'den gelen Markdown yanıtı
    }
}