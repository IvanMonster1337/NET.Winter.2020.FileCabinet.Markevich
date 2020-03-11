﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using FileCabinetApp;

namespace FileCabinetGenerator.Generator
{
    public static class RecordGenerator
    {
        private static string[] names =
        {
            "Alexei",
            "Adrian",
            "Dima",
            "Igor",
            "Ivan",
            "Maxim",
            "Michail",
            "Nicholai",
            "Oleg",
            "Sasha",
            "Timofey",
            "Viktor"
        };

        private static string[] surnames =
        {
            "Alexeev",
            "Andreyev",
            "Baranov",
            "Bobrov",
            "Bogomolov",
            "Bykov",
            "Chernov",
            "Chugunkin",
            "Drozdov",
            "Egorov",
            "Fedorov",
            "Golubev",
            "Gorky",
            "Gusev",
            "Ibragimov",
            "Ivanov",
            "Kalashnik",
            "Kamenev",
            "Kotov",
            "Kozlov",
            "Krovopuskov",
            "Kuznetsov",
            "Krupin",
            "Lagunov",
            "Lebedev",
            "Medvedev",
            "Meknikov",
            "Mikhailov",
            "Molchalin",
            "Molotov"
        };

        private static char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();

        private static Random gen = new Random();

        public static IReadOnlyCollection<FileCabinetRecord> Generate(int amount, int startId)
        {
            List<FileCabinetRecord> returnList = new List<FileCabinetRecord>();
            for(int i=0;i<amount;i++)
            {
                returnList.Add(GenerateRecord(startId+i));
            }

            return returnList;
        }

        private static FileCabinetRecord GenerateRecord(int id)
        {
            FileCabinetRecord returnRecord = new FileCabinetRecord();
            returnRecord.Id = id;
            returnRecord.FirstName = names[gen.Next(names.Length - 1)];
            returnRecord.LastName = surnames[gen.Next(surnames.Length - 1)];
            returnRecord.Code = (short)gen.Next(32766);
            returnRecord.Letter = chars[gen.Next(chars.Length - 1)];
            returnRecord.Balance = new decimal(gen.Next(),
                gen.Next(),
                gen.Next(0x204FCE5E),
                false, 0);
            returnRecord.DateOfBirth = RandomDate();
            return returnRecord;
        }

        private static DateTime RandomDate()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
