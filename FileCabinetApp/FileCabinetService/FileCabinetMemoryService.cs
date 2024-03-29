﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Iterators;
using FileCabinetApp.RecordValidator;

namespace FileCabinetApp
{
    #pragma warning disable CA1303 // Do not pass literals as localized parameters
    /// <summary>Class for working with the file cabinet.</summary>
    public abstract class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly RecordValidator.IRecordValidator validator;

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary =
            new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary =
            new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary =
            new Dictionary<DateTime, List<FileCabinetRecord>>();

        private List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private Dictionary<string, string> selectDictionary =
            new Dictionary<string, string>();

        /// <summary>Initializes a new instance of the <see cref="FileCabinetMemoryService"/> class.</summary>
        /// <param name="recordValidator">The record validator.</param>
        protected FileCabinetMemoryService(RecordValidator.IRecordValidator recordValidator)
        {
            this.validator = recordValidator;
        }

        /// <summary>Gets the validator.</summary>
        /// <value>The validator.</value>
        /// <returns>Validator for this class.</returns>
        public IRecordValidator GetValidator()
        {
            return this.validator;
        }

        /// <summary>Creates the record.</summary>
        /// <param name="newRecordData">Container for the record's fields.</param>
        /// <returns>Returns the new record's ID.</returns>
        public int CreateRecord(RecordData newRecordData)
        {
            if (newRecordData != null)
            {
                this.validator.Validate(newRecordData.FirstName, newRecordData.LastName, newRecordData.Code, newRecordData.Letter, newRecordData.Balance, newRecordData.DateOfBirth);
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

                this.selectDictionary = new Dictionary<string, string>();
                return record.Id;
            }

            throw new ArgumentNullException(nameof(newRecordData), $"{nameof(newRecordData)} is null.");
        }

        /// <summary>Edits the record.</summary>
        /// <param name="newRecordData">Container for the record's fields.</param>
        /// <exception cref="ArgumentException">Thrown when id is incorrect.</exception>
        public void EditRecord(RecordData newRecordData)
        {
            if (newRecordData != null)
            {
                this.validator.Validate(newRecordData.FirstName, newRecordData.LastName, newRecordData.Code, newRecordData.Letter, newRecordData.Balance, newRecordData.DateOfBirth);
                foreach (var record in this.list)
                {
                    if (record.Id == newRecordData.Id)
                    {
                        if (this.firstNameDictionary[record.FirstName].Count > 1)
                        {
                            this.firstNameDictionary[record.FirstName].Remove(record);
                        }
                        else
                        {
                            this.firstNameDictionary.Remove(record.FirstName);
                        }

                        if (this.lastNameDictionary[record.LastName].Count > 1)
                        {
                            this.lastNameDictionary[record.LastName].Remove(record);
                        }
                        else
                        {
                            this.lastNameDictionary.Remove(record.LastName);
                        }

                        if (this.dateOfBirthDictionary[record.DateOfBirth].Count > 1)
                        {
                            this.dateOfBirthDictionary[record.DateOfBirth].Remove(record);
                        }
                        else
                        {
                            this.dateOfBirthDictionary.Remove(record.DateOfBirth);
                        }

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

                        this.selectDictionary = new Dictionary<string, string>();
                        return;
                    }
                }

                throw new ArgumentException($"{nameof(newRecordData.Id)} is incorrect.");
            }
        }

        /// <summary>Deletes the record with the specified id.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentException">Id #{id} doesn't exist.</exception>
        public void DeleteRecord(int id)
        {
            foreach (var element in this.list)
            {
                if (element.Id == id)
                {
                    this.firstNameDictionary[element.FirstName].Remove(element);
                    this.lastNameDictionary[element.LastName].Remove(element);
                    this.dateOfBirthDictionary[element.DateOfBirth].Remove(element);
                    this.list.Remove(element);
                    this.selectDictionary = new Dictionary<string, string>();
                    return;
                }
            }

            throw new ArgumentException($"Id #{id} doesn't exist.");
        }

        /// <summary>Finds the record by its first name.</summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>The array of record with specific first name.</returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.firstNameDictionary.Keys)
            {
                if (firstName.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    resultList = this.firstNameDictionary[key];
                }
            }

            foreach (var element in new MemoryCollection(resultList))
            {
                yield return element;
            }
        }

        /// <summary>Finds the record by its last name.</summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>The array of record with specific last name.</returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.lastNameDictionary.Keys)
            {
                if (lastName.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    resultList = this.lastNameDictionary[key];
                }
            }

            foreach (var element in new MemoryCollection(resultList))
            {
                yield return element;
            }
        }

        /// <summary>Finds the record by its date of birth.</summary>
        /// <param name="dateTime">The date of birth.</param>
        /// <returns>The array of record with specific date of birth.</returns>
        public IEnumerable<FileCabinetRecord> FindByDateOfBirth(DateTime dateTime)
        {
            List<FileCabinetRecord> resultList = new List<FileCabinetRecord>();
            foreach (var key in this.dateOfBirthDictionary.Keys)
            {
                if (dateTime.Equals(key))
                {
                    resultList = this.dateOfBirthDictionary[key];
                }
            }

            foreach (var element in new MemoryCollection(resultList))
            {
                yield return element;
            }
        }

        /// <summary>Gets all the records.</summary>
        /// <returns>An array of records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.list);
        }

        /// <summary>Makes the snapshot.</summary>
        /// <returns>Returns the snapshot of current FileCabinet.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list);
        }

        /// <summary>Restores the FileCabinet with specified snapshot.</summary>
        /// <param name="snapshot">The snapshot.</param>
        public void Restore(FileCabinetServiceSnapshot snapshot)
        {
            if (snapshot != null)
            {
                this.list = new List<FileCabinetRecord>(snapshot.Records);
            }
        }

        /// <summary>Gets the stat.</summary>
        /// <returns>The number of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>Gets the removed stat.</summary>
        /// <returns>Returns the count of removed records.</returns>
        public int GetRemovedStat()
        {
            return 0;
        }

        /// <summary>Purges this instance.</summary>
        /// <returns>Amount of purged elements.</returns>
        /// <exception cref="NotImplementedException">This method is incorrect for this type of FileCabinet.</exception>
        public int Purge()
        {
            throw new NotImplementedException("This method is incorrect for this type of FileCabinet.");
        }

        /// <summary>Gets the select dictionary.</summary>
        /// <returns>Returns the select dictionary.</returns>
        public Dictionary<string, string> GetSelectDictionary()
        {
            return this.selectDictionary;
        }
    }
}