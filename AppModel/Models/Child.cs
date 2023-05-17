using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Models
{
    public class Child : Entity
    {
        private string _firstName;

        public virtual string firstName { get => _firstName; set => _firstName = value; }
        private string _lastName;
        public virtual string lastName { get => _lastName; set => _lastName = value; }
        private int _age;
        public virtual int age { get => _age; set => _age = value; }

        private int _noTrials = 0;
        public virtual int noTrials {get => _noTrials; set => _noTrials = value; }

    public Child() { }
    public Child(string firstName, string lastName, int age)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._age = age;
        }

        public Child(string firstName, string lastName, int age, int noTrials)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._age = age;
            this._noTrials = noTrials;
        }

        public virtual String getFirstName()
        {
            return this._firstName;
        }

        public virtual String getLastName()
        {
            return this._lastName;
        }

        public virtual int getAge()
        {
            return this._age;
        }

        public virtual int getNoTrials() 
        {
            return this._noTrials;
        }

        public virtual void setNoTrials(int noTrials)
        {
            this._noTrials = noTrials;
        }

        public virtual void setFirstName(String givenFirstName)
        {
            this._firstName = givenFirstName;
        }

        public virtual void setLastName(String givenLastName)
        {
            this._lastName = givenLastName;
        }

        public virtual void setAge(int givenAge)
        {
            this._age = givenAge;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is Child child &&
        //           firstName == child._firstName &&
        //           lastName == child._lastName &&
        //           age == child._age;
        //}

        //public override int GetHashCode()
        //{
        //    return Hash(_firstName, _lastName, _age);
        //}
    }
}
