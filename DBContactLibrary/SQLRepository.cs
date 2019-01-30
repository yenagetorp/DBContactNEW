using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using DBContactLibrary.Models;

namespace DBContactLibrary
{

    public class SQLRepository
    {
        string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBContact;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private int CreateRecord(string sp_Name, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = sp_Name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddRange(parameters);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    if (returnValue > 0)
                    {
                        return (int)command.Parameters[parameters.Length - 1].Value;

                    }

                    else return 0;

                }
            }
        }




        public int CreateContact(string ssn, string firstName, string lastName)
        {
            SqlParameter[] parameters =
                {
                new SqlParameter("@ssn", ssn),
                new SqlParameter("@firstName", firstName),
                new SqlParameter("@lastName", lastName),
                new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output}
                };

            return CreateRecord("CreateContact", parameters);
        }



        //public int CreateContact(string ssn, string firstName, string lastName)
        //{
        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand())
        //        {
        //            command.CommandText = "CreateContact";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Connection = connection;

        //            SqlParameter sqlSsn = new SqlParameter("@ssn", ssn);
        //            SqlParameter sqlFirstName = new SqlParameter("@firstName", firstName);
        //            SqlParameter sqlLastName = new SqlParameter("@lastName", lastName);

        //            SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int)
        //            {
        //                Direction = ParameterDirection.Output
        //            };

        //            command.Parameters.Add(sqlSsn);
        //            command.Parameters.Add(sqlFirstName);
        //            command.Parameters.Add(sqlLastName);
        //            command.Parameters.Add(sqlId);

        //            int returnValue = command.ExecuteNonQuery();

        //            connection.Close();
        //            if (returnValue > 0)
        //            {
        //                return int.Parse(sqlId.Value.ToString());

        //            }

        //            else return 0;

        //        }

        //    }
        //}// Returns ID

        public Contact ReadContact(int ID)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "ReadContact";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@id", ID);

                    command.Parameters.Add(sqlId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Contact contact = new Contact
                        {
                            ID = (int)reader["ID"],
                            SSN = (string)reader["SSN"],//(string) cast
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"]
                        };

                        connection.Close();
                        return contact;

                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                }


            }

        }


        public List<Contact> ReadAllContacts()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Select * from Contact";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<Contact> contacts = new List<Contact>();
                        //loppar igenom alla poster in list Contact;
                        while (reader.Read())
                        {
                            var contact = new Contact
                            {
                                ID = (int)reader["ID"],
                                SSN = (string)reader["SSN"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"]
                            };
                            contacts.Add(contact);
                        };


                        connection.Close();
                        return contacts;

                    }
                    connection.Close();

                    return null;
                }

            }
        }

        public bool UpdateContact(int Id, string ssn, string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UpdateContact";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlSsn = new SqlParameter("@ssn", ssn);
                    SqlParameter sqlFirstName = new SqlParameter("@firstName", firstName);
                    SqlParameter sqlLastName = new SqlParameter("@lastName", lastName);

                    SqlParameter sqlId = new SqlParameter("@ID", Id);


                    command.Parameters.Add(sqlSsn);
                    command.Parameters.Add(sqlFirstName);
                    command.Parameters.Add(sqlLastName);
                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    return returnValue > 0;
                    //if (returnValue > 0)
                    //{
                    //    return true;

                    //}

                    //else return false;

                }

            }
        }

        public bool DeleteContact(int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DeleteContact";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;



                    SqlParameter sqlId = new SqlParameter("@ID", id);



                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    return returnValue > 0;
                    //if (returnValue > 0)
                    //{
                    //    return true;

                    //}

                    //else return false;

                }

            }
        }

        public int CreateAddress(string street, string city, string zip)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "CreateAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlStreet = new SqlParameter("@street", street);
                    SqlParameter sqlCity = new SqlParameter("@city", city);
                    SqlParameter sqlZip = new SqlParameter("@zip", zip);

                    SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(sqlStreet);
                    command.Parameters.Add(sqlCity);
                    command.Parameters.Add(sqlZip);
                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();//returns a number of rows been affacted

                    connection.Close();
                    if (returnValue > 0)
                        return int.Parse(sqlId.Value.ToString());
                    else return 0;
                }

            }
        }// Returns ID

        public Address ReadAddress(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "ReadAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    SqlParameter sqlId = new SqlParameter("@id", Id);
                    command.Parameters.Add(sqlId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Address address = new Address
                        {
                            ID = (int)reader["Id"],
                            Street = (string)reader["Street"],
                            City = (string)reader["City"],
                            Zip = (string)reader["Zip"]
                        };
                        connection.Close();
                        return address;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                }


            }

        }

        public List<Address> ReadAllAddresses()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from Address";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<Address> addresses = new List<Address>();
                        while (reader.Read())
                        {
                            addresses.Add(new Address
                            {
                                ID = (int)reader["Id"],
                                Street = (string)reader["Street"],
                                City = (string)reader["City"],
                                Zip = (string)reader["Zip"]
                            });
                        }

                        connection.Close();
                        return addresses;
                    }
                    else
                    {
                        connection.Close();
                        return null;

                    }

                }
            }
        }

        public bool UpdateAddress(int Id, string street, string city, string zip)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UpdateAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@Id", Id);
                    SqlParameter sqlStreet = new SqlParameter("@street", street);
                    SqlParameter sqlCity = new SqlParameter("@city", city);
                    SqlParameter sqlZip = new SqlParameter("@zip", zip);

                    command.Parameters.Add(sqlId);
                    command.Parameters.Add(sqlStreet);
                    command.Parameters.Add(sqlCity);
                    command.Parameters.Add(sqlZip);

                    int returnValue = command.ExecuteNonQuery();//return a number of rows been affected.

                    connection.Close();
                    return returnValue > 0;
                }
            }
        }


        public bool DeleteAddresses(int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DeleteAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;



                    SqlParameter sqlId = new SqlParameter("@ID", id);



                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    return returnValue > 0;
                }

            }
        }

        //contactinformation tabel
        public int CreateContactToAddress(int contactId, int addressId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "CreateContactToAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlContactId = new SqlParameter("@contactId", contactId);
                    SqlParameter sqlAddressId = new SqlParameter("@addressId", addressId);

                    SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(sqlContactId);
                    command.Parameters.Add(sqlAddressId);
                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    if (returnValue > 0)
                    {
                        return int.Parse(sqlId.Value.ToString());

                    }

                    else return 0;

                }

            }
        }// Returns ID

        public ContactToAddress ReadContactToAddress(int ID)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "ReadContactToAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@id", ID);

                    command.Parameters.Add(sqlId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var contactToAddress = new ContactToAddress
                        {
                            ID = (int)reader["ID"],
                            ContactID = (int)reader["ContactID"],
                            AddressID = (int)reader["AddressID"]
                        };

                        connection.Close();
                        return contactToAddress;

                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                }


            }

        }


        public List<ContactToAddress> ReadAllContactToAddress()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from ContactToAddress";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<ContactToAddress> contactToAddresses = new List<ContactToAddress>();
                        while (reader.Read())
                        {
                            contactToAddresses.Add(new ContactToAddress
                            {
                                ID = (int)reader["ID"],
                                ContactID = (int)reader["ContactID"],
                                AddressID = (int)reader["AddressID"]
                            });
                        }

                        connection.Close();
                        return contactToAddresses;
                    }
                    else
                    {
                        connection.Close();
                        return null;

                    }

                }
            }
        }

        public bool UpdateContactToAddress(int Id, int contactId, int addressId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UpdateContactToAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@Id", Id);
                    SqlParameter sqlContactId = new SqlParameter("@contactId", contactId);
                    SqlParameter sqlAddressId = new SqlParameter("@addressId", addressId);

                    command.Parameters.Add(sqlId);
                    command.Parameters.Add(sqlContactId);
                    command.Parameters.Add(sqlAddressId);

                    int returnValue = command.ExecuteNonQuery();//return a number of rows been affected.

                    connection.Close();
                    return returnValue > 0;
                }
            }
        }

        public bool DeleteContactToAddress(int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DeleteContactToAddress";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;



                    SqlParameter sqlId = new SqlParameter("@ID", id);



                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    return returnValue > 0;
                }

            }
        }

        public int CreateContactInformation(string info, int contactId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "CreateContactInformation";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlContactId = new SqlParameter("@contactId", contactId);
                    SqlParameter sqlInfo = new SqlParameter("@info", info);

                    SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(sqlContactId);
                    command.Parameters.Add(sqlInfo);
                    command.Parameters.Add(sqlId);

                    int returnValue = command.ExecuteNonQuery();

                    connection.Close();
                    if (returnValue > 0)
                    {
                        return int.Parse(sqlId.Value.ToString());

                    }

                    else return 0;

                }

            }
        }// Returns ID

        public ContactInformation ReadContactInformation(int ID)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "ReadContactInformation";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@id", ID);

                    command.Parameters.Add(sqlId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var contactInfo = new ContactInformation
                        {
                            ID = (int)reader["ID"],
                            Info = (string)reader["Info"],

                            ContactID = (int)reader["ContactID"]
                        };

                        connection.Close();
                        return contactInfo;

                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                }


            }

        }


        public List<ContactInformation> ReadAllContactInformation()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from ContactInformation";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<ContactInformation> contactInformations = new List<ContactInformation>();

                        while (reader.Read())
                        {
                            contactInformations.Add(new ContactInformation
                            {
                                ID = (int)reader["ID"],
                                Info = (string)reader["Info"],

                                ContactID = reader["ContactID"] as int?
                            });
                        }

                        connection.Close();

                        return contactInformations;

                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
        }

        //public bool UpdateContactToAddress(int Id, int contactId, int addressId)
        //{
        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand())
        //        {
        //            command.CommandText = "UpdateContactToAddress";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Connection = connection;

        //            SqlParameter sqlId = new SqlParameter("@Id", Id);
        //            SqlParameter sqlContactId = new SqlParameter("@contactId", contactId);
        //            SqlParameter sqlAddressId = new SqlParameter("@addressId", addressId);

        //            command.Parameters.Add(sqlId);
        //            command.Parameters.Add(sqlContactId);
        //            command.Parameters.Add(sqlAddressId);

        //            int returnValue = command.ExecuteNonQuery();//return a number of rows been affected.

        //            connection.Close();
        //            return returnValue > 0;
        //        }
        //    }
        //}

        //public bool DeleteContactToAddress(int id)
        //{
        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand())
        //        {
        //            command.CommandText = "DeleteContactToAddress";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Connection = connection;



        //            SqlParameter sqlId = new SqlParameter("@ID", id);



        //            command.Parameters.Add(sqlId);

        //            int returnValue = command.ExecuteNonQuery();

        //            connection.Close();
        //            return returnValue > 0;
        //        }

        //    }
        //}
    }
}
