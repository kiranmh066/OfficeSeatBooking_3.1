using Microsoft.EntityFrameworkCore;
using Office_Seat_Book_Entity;
using System.Collections.Generic;
using System.Linq;

namespace Office_Seat_Book_DLL.Repost
{
    public class SecretKeyRepost : ISecretKeyRepost
    {
        Office_DB_Context _dbContext;//default ecretKey

        public SecretKeyRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddSecretKey(SecretKey secretKey)
        {
            _dbContext.secretKey.Add(secretKey);
            _dbContext.SaveChanges();
        }

        public void DeleteSecretKey(int secretKeyId)
        {
            var secretKey = _dbContext.secretKey.Find(secretKeyId);
            _dbContext.secretKey.Remove(secretKey);
            _dbContext.SaveChanges();
        }

        public SecretKey GetSecretKeyById(int secretKeyId)
        {
            return _dbContext.secretKey.Find(secretKeyId);
        }

        public IEnumerable<SecretKey> GetSecretKeys()
        {
            return _dbContext.secretKey.ToList();
        }


        public void UpdateSecretKey(SecretKey secretKey)
        {

            _dbContext.Entry(secretKey).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public SecretKey GetSecretKeyByEmpId(int empId)
        {
            List<SecretKey> secretKeys = _dbContext.secretKey.Include(obj => obj.Employee.EmpID).ToList();

            List<SecretKey> secretKeyList = new List<SecretKey>();
            foreach (var item in secretKeys)
            {
                if (empId == item.EmpID)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
