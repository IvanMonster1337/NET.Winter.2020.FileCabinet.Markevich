﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>Class for working with the file cabinet.</summary>
    public abstract class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary =
            new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary =
            new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary =
            new Dictionary<DateTime, List<FileCabinetRecord>>();

        /// <summary>Creates the record.</summary>
        /// <param name="newRecordData">Container for the record's fields.</param>
        /// <returns>Returns the new record's ID.</returns>
        public int CreateRecord(RecordData newRecordData)
        {
            this.CheckParameters(newRecordData.FirstName, newRecordData.LastName, newRecordData.Code,
                newRecordData.Letter, newRecordData.Balance, newRecordData.DateOfBirth);
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = newRecordData.FirstName,
                LastName = newRecordData.LastName,
                Code = newRecordData.Code,
                Letter = newRecordData.Letter,
                Balance = newRecordData.Balance,
                DateOfBirth = newRecordData.DateOfBirth,
            };

            this.list.Add(record);
            if (!this.firstNameDictionary.ContainsKey(newRecordData.FirstName))
            {
                this.firstNameDictionary.Add(newRecordData.FirstName, new List<FileCabinetRecord>());
                this.firstNameDictionary[newRecordData.FirstName].Add(record);
            }
            else
            {
                this.firstNameDictionary[newRecordData.FirstName].Add(record);
            }

            if (!this.lastNameDictionary.ContainsKey(newRecordData.LastName))
            {
                this.lastNameDictionary.Add(newRecordData.LastName, new List<FileCabinetRecord>());
                this.lastNameDictionary[newRecordData.LastName].Add(record);
            }
            else
            {
                this.lastNameDictionary[newRecordData.LastName].Add(record);
            }

            if (!this.dateOfBirthDictionary.ContainsKey(newRecordData.DateOfBirth))
            {
                this.dateOfBirthDictionary.Add(newRecordData.DateOfBirth, new List<FileCabinetRecord>());
                this.dateOfBirthDictionary[newRecordData.DateOfBirth].Add(record);
            }
            else
            {
                this.dateOfBirthDictionary[newRecordData.DateOfBirth].Add(record);
            }

            return record.Id;
        }

        /// <summary>Edits the record.</summary>
        /// <param name="newRecordData">Container for the record's fields.</param>
        /// <exception cref="ArgumentException">Thrown when id is incorrect.</exception>
        public void EditRecord(RecordData newRecordData)
        {
            this.CheckParameters(newRecordData.FirstName, newRecordData.LastName, newRecordData.Code,
                newRecordData.Letter, newRecordData.Balance, newRecordData.DateOfBirth);
            foreach (var record in this.list)
            {
                if (record.Id == newRecordData.Id)
                {
                    this.firstNameDictionary.Remove(record.FirstName);
                    record.FirstName = newRecordData.FirstName;
                    record.LastName = newRecordData.LastName;
                    record.Code = newRecordData.Code;
                    record.Letter = newRecordData.Letter;
                    record.Balance = newRecordData.Balance;
                    record.DateOfBirth = newRecordData.DateOfBirth;
                    if (!this.firstNameDictionary.ContainsKey(newRecordData.FirstName))
                    {
                        this.firstNameDictionary.Add(newRecordData.FirstName, new List<FileCabinetRecord>());
                        this.firstNameDictionary[newRecordData.FirstName].Add(record);
                    }
                    else
                    {
                        this.firstNameDictionary[newRecordData.FirstName].Add(record);
                    }

                    if (!this.lastNameDictionary.ContainsKey(newRecordData.LastName))
                    {
                        this.lastNameDictionary.Add(newRecordData.LastName, new List<FileCabinetRecord>());
                        this.lastNameDictionary[newRecordData.LastName].Add(record);
                    }
                    else
                    {
                        this.lastNameDictionary[newRecordData.LastName].Add(record);
                    }

                    if (!this.dateOfBirthDictionary.ContainsKey(newRecordData.DateOfBirth))
                    {
                        this.dateOfBirthDictionary.Add(newRecordData.DateOfBirth, new List<FileCabinetRecord>());
                        this.dateOfBirthDictionary[newRecordData.DateOfBirth].Add(record);
                    }
                    else
                    {
                        this.dateOfBirthDictionary[newRecordData.DateOfBirth].Add(record);
                    }

                    return;
                }
            }

            throw new ArgumentException($"{nameof(newRecordData.Id)} is incorrect.");
        }

        /// <summary>Finds the record by its first name.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>The array of record with specific first name.</returns>
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.firstNameDictionary.Keys)
            {
                if (firstName.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    resultList = this.firstNameDictionary[key];
                }
            }

            return resultList.ToArray();
        }

        /// <summary>Finds the record by its last name.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>The array of record with specific last name.</returns>
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.lastNameDictionary.Keys)
            {
                if (lastName.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    resultList = this.lastNameDictionary[key];
                }
            }

            return resultList.ToArray();
        }

        /// <summary>Finds the record by its date of birth.</summary>
        /// <param name="dateTimee">The date of birth.</param>
        /// <returns>The array of record with specific date of birth.</returns>
        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateTime)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.dateOfBirthDictionary.Keys)
            {
                if (dateTime.Equals(key))
                {
                    resultList = this.dateOfBirthDictionary[key];
                }
            }

            return resultList.ToArray();
        }

        /// <summary>Gets all the records.</summary>
        /// <returns>An array of records.</returns>
        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        /// <summary>Gets the stat.</summary>
        /// <returns>The number of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>Checks the parameters.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="code">The code.</param>
        /// <param name="letter">The letter.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <exception cref="ArgumentException">Thrown when one of the parameters is incorrect.</exception>
        public abstract void CheckParameters(string firstName, string lastName, short code, char letter, decimal balance, DateTime dateOfBirth);
    }
}