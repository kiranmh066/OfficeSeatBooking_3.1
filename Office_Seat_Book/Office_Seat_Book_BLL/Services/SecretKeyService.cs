using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Office_Seat_Book_BLL.Services
{
    public class SecretKeyService
    {
        ISecretKeyRepost _SecretKeyRepost;
        public SecretKeyService(ISecretKeyRepost secretKeyRepost)
        {
            _SecretKeyRepost = secretKeyRepost;
        }

        //Add Appointment
        public void AddSecretKey(SecretKey secretKey)
        {
            _SecretKeyRepost.AddSecretKey(secretKey);
        }

        //Delete Appointment


        public void DeleteSecretKey(int secretKeyID)
        {
            _SecretKeyRepost.DeleteSecretKey(secretKeyID);
        }

        //Update Appointment

        public void UpdateSecretKey(SecretKey SecretKey)
        {
            _SecretKeyRepost.UpdateSecretKey(SecretKey);
        }

        //Get getAppointments

        public IEnumerable<SecretKey> GetSecretKey()
        {
            return _SecretKeyRepost.GetSecretKeys();
        }
        public SecretKey GetBySecretKeyId(int SecretKeyID)
        {
            return _SecretKeyRepost.GetSecretKeyById(SecretKeyID);
        }
        public SecretKey GetSecretKeyByEmpId(int empId)
        {
            return _SecretKeyRepost.GetSecretKeyByEmpId(empId);
        }
    }
}
