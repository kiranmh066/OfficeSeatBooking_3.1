using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public interface ISecretKeyRepost
    {
        void AddSecretKey(SecretKey secretKey);
        void UpdateSecretKey(SecretKey secretKey);

        void DeleteSecretKey(int secretKeyId);

        SecretKey GetSecretKeyById(int secretKeyId);

        IEnumerable<SecretKey> GetSecretKeys();

        SecretKey GetSecretKeyByEmpId(int empId);
    }
}
