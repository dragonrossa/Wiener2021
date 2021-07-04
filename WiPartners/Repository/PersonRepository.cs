using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WiPartners.Models;

namespace WiPartners.Repository
{
    // for database related operations
    public class PersonRepository
    {
        public SqlConnection con;
        //To Handle connection related activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }

        public List<PartnerPersonalData> ReadData()
        {
            List<PartnerPersonalData> datas = new List<PartnerPersonalData>();
            try
            {
                connection();
                con.Open();
                var list = con.Query<PartnerPersonalData>(
                    "SELECT ID, FirstName, LastName, Adress, CroatianPIN, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender, PartnerNumber, PartnerTypeId FROM PersonalData JOIN PartnerPersonalData ON PartnerPersonalData.PersonalDataID = PersonalData.ID  ORDER BY PersonalData.ID DESC"
                    ).ToList();
                foreach (var item in list)
                {
                    datas.Add(item);
                }

                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return datas;
        }

        public List<PartnerPersonalData> CreateNewPartner(PartnerPersonalData partner)
        {
            List<PartnerPersonalData> datas = new List<PartnerPersonalData>();
            try
            {
                connection();
                con.Open();

                //PersonalData
                var firstName = partner.FirstName;
                var lastName = partner.LastName;
                var adress = partner.Adress;
                var croatianPIN = partner.CroatianPIN;
                var createdAtUtc = partner.CreatedAtUtc;
                var createByUser = partner.CreateByUser;
                var isForeign = partner.IsForeign;
                var externalCode = partner.ExternalCode;
                var gender = partner.Gender;


                //PartnerPersonalData
                var partnerNumber = partner.PartnerNumber;
                var partnerTypeId = partner.PartnerTypeId;


                var parameters = new { 
                    firstName = firstName, 
                    lastName = lastName, 
                    adress = adress, 
                    croatianPIN = croatianPIN, 
                    createdAtUtc = createdAtUtc,
                    createByUser = createByUser,
                    isForeign = isForeign,
                    externalCode = externalCode,
                    gender = gender
                };
                var insertDataToPersonalDataAndGetId = "INSERT INTO PersonalData(FirstName, LastName, Adress, CroatianPIN, CreatedAtUtc, " +
                    "CreateByUser, IsForeign, ExternalCode, Gender) " +
                    "VALUES(@firstName, @lastName, @adress, @croatianPIN, @createdAtUtc, @createByUser, @isForeign, @externalCode, @gender);" +
                    "SELECT SCOPE_IDENTITY();";

                var result = con.QueryMultiple(insertDataToPersonalDataAndGetId, parameters);

                var personalDataID = result.Read<int>(); //get id of last input

                var partnerParameters = new
                {
                    partnerNumber = partnerNumber,
                    partnerTypeId = partnerTypeId,
                    personalDataID = personalDataID
                };

                var insertDataToPartnerPersonalData = "INSERT INTO PartnerPersonalData VALUES(@partnerNumber, @partnerTypeId, @personalDataID)";

                con.QueryMultiple(insertDataToPartnerPersonalData, partnerParameters);

                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return datas;
        }


    }
}