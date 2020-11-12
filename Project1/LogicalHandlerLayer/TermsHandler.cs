using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class TermsHandler
    {
        TermDA termDA = new TermDA();

        public void AddTerm(Term term)
        {
            termDA.AddTerm(term);
        }

        public void UpdateTerm(string id, Term newInfo)
        {
            termDA.UpdateTerm(id, newInfo);
        }

        public void DeleteTerm(string id)
        {
            termDA.DeleteTerm(id);
        }

        public List<Term> GetListTerm()
        {
            return termDA.GetTerms();
        }

        public List<Term> GetListTerm(int length)
        {
            return termDA.GetTerms(length);
        }

        public int GetTermIndex(string id)
        {
            List<Term> terms = GetListTerm();
            for (int i = 0; i < terms.Count; i++)
                if (id == terms[i].ID)
                    return i;
            return -1;
        }

        public Term GetTermInfo(string id)
        {
            List<Term> terms = new List<Term>();
            return terms[GetTermIndex(id)];
        }

        public bool CheckId(string id)
        {
            if (Regex.IsMatch(id, "[0-9][0-9][0-9]"))
                return true;
            return false;
        }

        public bool CheckCredit(int num)
        {
            if (num >= 1 && num <= 4)
                return true;
            return false;
        }

        public List<Term> GetList(string subjectID)
        {
            List<Term> terms = GetListTerm();
            List<Term> result = new List<Term>();
            foreach(var term in terms)
            {
                if (term.SubjectId == subjectID)
                    result.Add(term);
            }
            return result;
        }

        public Term GetTerm(string id)
        {
            List<Term> terms = GetListTerm();
            return terms[GetTermIndex(id)];
        }

        public Term GetTerm(string id, List<Term> terms)
        {
            foreach(var term in terms)
            {
                if (term.ID == id)
                    return term;
            }
            return null;
        }

        public int GetIndex(string id, List<Term> terms)
        {
            for(int i = 0; i<terms.Count; i++)
            {
                if (terms[i].ID == id)
                    return i;
            }
            return -1;
        }

    }
}
