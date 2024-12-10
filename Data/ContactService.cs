namespace BlazeBook.Data
{
    public interface IContactService
    {
        Task SaveContactAsync(Contact contact);
    }

    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
    }
}
