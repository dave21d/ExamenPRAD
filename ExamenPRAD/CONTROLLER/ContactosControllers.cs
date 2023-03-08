using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ExamenPRAD.MODELS;
using System.Threading.Tasks;

namespace ExamenPRAD.CONTROLLER
{
    public class ContactosControllers
    {
        public class SqliteHelper
        {
            public SQLiteAsyncConnection dbcontac;

            public SqliteHelper(string dbpath)
            {
                dbcontac = new SQLiteAsyncConnection(dbpath);
                dbcontac.CreateTableAsync<Contactos>().Wait();
            }
            public Task<int> SaveContact(Contactos contactos)
            {
                if (contactos.id != 0)

                    return dbcontac.UpdateAsync(contactos);


                else
                    return dbcontac.InsertAsync(contactos);


            }

            public Task<int> deletecontactosAsync(Contactos contactosmodels)
            {
                return dbcontac.DeleteAsync(contactosmodels);
            }

            public Task<List<Contactos>> GetContactoAsync()
            {
                return dbcontac.Table<Contactos>().ToListAsync();
            }
            public Task<Contactos> GetPersonaByIdAsync(int idcontacto)
            {
                return dbcontac.Table<Contactos>().Where(a => a.id == idcontacto).FirstOrDefaultAsync();
            }


        }
    }
}

