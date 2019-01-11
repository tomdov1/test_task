using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakyBox
{
    class Program
    {
        static void Main(string[] args)
        {
            DivideCondition divideCondition1 = new DivideCondition(3);
            DivideCondition divideCondition2 = new DivideCondition(5);

            PrintAction printAction1 = new PrintAction("sneaky");
            PrintAction printAction2 = new PrintAction("box");
            PrintAction printAction3 = new PrintAction("sneakybox");

            TwoConditionEvent loopEvent = new TwoConditionEvent(divideCondition1, divideCondition2, printAction1, printAction2, printAction3);

            for (int i = Params.LoopStart; i < Params.LoopEnd; i++)
            {
                divideCondition1.BaseNumber = i;
                divideCondition2.BaseNumber = i;

                printAction1.SetExtras(i.ToString());
                printAction2.SetExtras(i.ToString());
                printAction3.SetExtras(i.ToString());

                loopEvent.StartEvent();
            }

            Console.ReadKey();
        }
    }

    interface IActions
    {
        void Execute();
    }

    interface IConditions
    {
        bool ConditionResult();
    }

    interface IEvents
    {
        void StartEvent();
    }


    /// <summary>
    ///  Corresponding action is executed for a condition, if both are met a third action is executed
    /// </summary>
    class TwoConditionEvent : IEvents
    {
        public IConditions Condition1 { get; private set; }
        public IConditions Condition2 { get; private set; }
        public IActions Action1 { get; private set; }
        public IActions Action2 { get; private set; }
        public IActions Action3 { get; private set; }

        public TwoConditionEvent( IConditions condition1, IConditions condition2, IActions action1, IActions action2, IActions action3)
        {
            Condition1 = condition1;
            Condition2 = condition2;
            Action1 = action1;
            Action2 = action2;
            Action3 = action3;
        }

        public void StartEvent()
        {
            if (Condition1.ConditionResult() && Condition2.ConditionResult())
            {
                Action3.Execute();
            }
            else if (Condition1.ConditionResult())
            {
                Action1.Execute();
            }
            else if (Condition2.ConditionResult())
            {
                Action2.Execute();
            }

        }
    }

    /// <summary>
    /// Checks if a BaseNumber divides from Divider without a remainder
    /// </summary>
    class DivideCondition : IConditions
    {
        public int BaseNumber { get; set; }
        public int Divider { get; private set; }

        public DivideCondition(int divider)
        {
            Divider = divider;
        }
        public bool ConditionResult()
        {
            if (BaseNumber % Divider == 0 && BaseNumber != 0)
                return true;
            else
                return false;
        }
    }


    /// <summary>
    /// Prints set line with extra content in front of it if needed
    /// </summary>
    class PrintAction : IActions
    {
        public string PrintContent { get; private set; }
        public string Extras { get; private set; }

        public PrintAction() { }

        public PrintAction(string line)
        {
            PrintContent = line;
            Extras = "";
        }

        public void SetExtras(string line)
        {
            Extras = line; 
        }

        public void Execute()
        {
            Console.WriteLine(Extras + " - " + PrintContent);
        }
    }


    /// <summary>
    /// Parameters for loop
    /// </summary>
    class Params
    {
        static public int LoopStart = 0;
        static public int LoopEnd = 50;
    }
}
