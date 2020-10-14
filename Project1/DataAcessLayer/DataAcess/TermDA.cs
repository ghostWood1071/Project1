using Project1.DataAcessLayer.Model;
using Project1.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class TermDA
    {
        string fileName = StringSource.TERM_DB_NAME;

        public List<Term> GetTerms()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Term> terms = new List<Term>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] info = line.Split('|');
                    terms.Add(new Term(info[0], info[1], int.Parse(info[2])));
                    line = reader.ReadLine();
                }
                reader.Close();
                return terms;
            }
        }

        public List<Term> GetTerms(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Term> terms = new List<Term>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                for (int i = 0; i < length; i++)
                {
                    string line = reader.ReadLine();
                    string[] info = line.Split('|');
                    terms.Add(new Term(info[0], info[1], int.Parse(info[2])));
                }
                reader.Close();
                return terms;
            }
        }

        public void SaveAllData(List<Term> terms)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var Term in terms)
                    writer.WriteLine(Term.ID + "|" + Term.Name + "|" + Term.CreditNum);
                writer.Flush();
                writer.Close();
            }
        }

        public void AddTerm(Term Term)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(Term.ID + "|" + Term.Name + "|" + Term.CreditNum);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetTermIndex(string id)
        {
            List<Term> terms = GetTerms();
            for (int i = 0; i < terms.Count; i++)
            {
                if (terms[i].ID == id)
                    return i;
            }
            return -1;
        }

        public void UpdateTerm(string id, Term newInfo)
        {
            int index = GetTermIndex(id);
            List<Term> terms = GetTerms();
            terms[index] = newInfo;
            SaveAllData(terms);
        }

        public void DeleteTerm(string id)
        {
            int index = GetTermIndex(id);
            List<Term> terms = GetTerms();
            terms.RemoveAt(index);
            SaveAllData(terms);
        }

    }
}
