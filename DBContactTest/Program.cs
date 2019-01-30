using System;
using DBContactLibrary;
using DBContactLibrary.Models;
/// <summary>
/// Ado.net
/// </summary>
namespace DBContactTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLRepository sqlRepository = new SQLRepository();
            //int myId = sqlRepository.CreateContact("19620601-1235", "Ulf", "Johansson");

            //int id = sqlRepository.CreateContact("19900923-2335", "Yen", "Svensson");
            //Console.WriteLine(id);
            //Contact contact1 = sqlRepository.ReadContact(id);

            //Console.WriteLine(contact1);
            // var contacts = sqlRepository.ReadAllContacts();
            //foreach (var contact in contacts)
            //{
            //    Console.WriteLine(contact);
            //}
            // sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);
            //sqlRepository.DeleteContact(id);
            //sqlRepository.UpdateContact(1, "19620601-1234", "Håkan", "Joelsson");
            //sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);

            //int id = sqlRepository.CreateAddress("SommarGatan", "Lund", "333 56");
            //Console.WriteLine(id);
            //Address address7 = sqlRepository.ReadAddress(7);
            //Console.WriteLine(address7);

            /* UppdateAddress */


            //sqlRepository.UpdateAddress(6, "RosenGatan", "Älmhult", "333 66");
            //var addresses1 = sqlRepository.ReadAllAddresses();
            //foreach (var Address in addresses1)
            //{
            //    Console.WriteLine(Address);
            //}

            /* delete addresses*/

            //sqlRepository.DeleteAddresses(6);

            //var addresses1 = sqlRepository.ReadAllAddresses();

            //foreach (var Address in addresses1)
            //{
            //    Console.WriteLine(Address);
            //}


            /*ContactToAddress*/
            //int ContactToAddressId = sqlRepository.CreateContactToAddress(4, 9);
            //Console.WriteLine(ContactToAddressId);

            //ContactToAddress contactToAddress = sqlRepository.ReadContactToAddress(4);
            //Console.WriteLine(contactToAddress);

            //var contactToAddresses = sqlRepository.ReadAllContactToAddress();
            //foreach (var contactToAddress in contactToAddresses)
            //{
            //    Console.WriteLine(contactToAddress);
            //}

            //sqlRepository.UpdateContactToAddress(1, 5, 1);

            //var contactToAddresses = sqlRepository.ReadAllContactToAddress();
            //foreach (var contactToAddress in contactToAddresses)
            //{
            //    Console.WriteLine(contactToAddress);
            //}
            //var contactinfo = sqlRepository.ReadContactInformation(1);
            //Console.WriteLine(contactinfo);
            var contactInfos = sqlRepository.ReadAllContactInformation();
            foreach (var info in contactInfos)
            {
                Console.WriteLine(info);
            }



        }
    }
}
