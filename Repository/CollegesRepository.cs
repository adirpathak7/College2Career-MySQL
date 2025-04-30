using College2Career.Data;
using College2Career.Models;

namespace College2Career.Repository
{
    public class CollegesRepository : ICollegesRepository
    {
        private readonly C2CDBContext c2CDBContext;

        public CollegesRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }

        public async Task addCollegesInformation(Colleges colleges)
        {
            try
            {
                await c2CDBContext.Colleges.AddAsync(colleges);
                await c2CDBContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine("ERROR occured in college repository in addInformation method: " + ex.Message);
            }
        }


    }
}
