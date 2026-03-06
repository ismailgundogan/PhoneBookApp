namespace PhoneBook.Core.Entity;
public class Personel
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string DahiliNo { get; set; } // Örn: 1024
    public DateTime? CreatedDate { get; set; } // Nullable kullanım örneği [cite: 19]
}
