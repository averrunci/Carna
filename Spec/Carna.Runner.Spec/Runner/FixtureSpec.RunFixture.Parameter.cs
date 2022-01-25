// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

[Context("Parameter")]
class FixtureSpec_RunFixture_Parameter : FixtureSteppable
{
    [Parameter("parameter1")]
    int parameter1 = 777;

    [Parameter]
    string parameter2;

    [Parameter]
    bool Parameter3 { get; } = true;

    [Parameter("parameter4")]
    double Parameter4 { get; }

    [Parameter("parameter5")]
    string CreateParameter5() => "Parameter5";

    [Parameter]
    int Parameter6() => 999;

    public FixtureSpec_RunFixture_Parameter()
    {
        parameter2 = "Parameter";
        Parameter4 = 7.77;
    }

    [Context("Parameters specified this constructor are equal to parameters specified by the parent fixture")]
    class Context01 : FixtureSteppable
    {
        private int Parameter1 { get; }
        private string Parameter2 { get; }
        private bool Parameter3 { get; }
        private double Parameter4 { get; }
        private string Parameter5 { get; }
        private int Parameter6 { get; }

        public Context01(int parameter1, string parameter2, bool Parameter3, double parameter4, string parameter5, int Parameter6)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            this.Parameter3 = Parameter3;
            Parameter4 = parameter4;
            Parameter5 = parameter5;
            this.Parameter6 = Parameter6;
        }

        [Example("When all parameters specified by the parent fixture are specified")]
        void Ex01()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
            Expect("the parameter3 should be the value specified by the parent", () => Parameter3 == true);
            Expect("the parameter4 should be the value specified by the parent", () => Parameter4 == 7.77);
            Expect("the parameter5 should be the value specified by the parent", () => Parameter5 == "Parameter5");
            Expect("the parameter6 should be the value specified by the parent", () => Parameter6 == 999);
        }

        [Example("When all parameters specified by the parent fixture are specified")]
        void Ex02()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
            Expect("the parameter3 should be the value specified by the parent", () => Parameter3 == true);
            Expect("the parameter4 should be the value specified by the parent", () => Parameter4 == 7.77);
            Expect("the parameter5 should be the value specified by the parent", () => Parameter5 == "Parameter5");
            Expect("the parameter6 should be the value specified by the parent", () => Parameter6 == 999);
        }
    }

    [Context("Parameters specified this constructor are less than parameters specified by the parent fixture")]
    class Context02 : FixtureSteppable
    {
        private int Parameter1 { get; }
        private string Parameter2 { get; }

        public Context02(int parameter1, string parameter2)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

        [Example("When some parameters specified by the parent fixture are specified")]
        void Ex01()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
        }

        [Example("When some parameters specified by the parent fixture are specified")]
        void Ex02()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
        }
    }

    [Context("Parameters specified this constructor are greater than parameters specified by the parent fixture")]
    class Context03 : FixtureSteppable
    {
        private int Parameter1 { get; }
        private string Parameter2 { get; }
        private bool Parameter3 { get; }
        private double Parameter4 { get; }
        private string Parameter5 { get; }
        private int Parameter6 { get; }

        public Context03(int Parameter6, string parameter2, object dummyParameter1, bool Parameter3, int parameter1, double parameter4, object dummyParameter2, string parameter5)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            this.Parameter3 = Parameter3;
            Parameter4 = parameter4;
            Parameter5 = parameter5;
            this.Parameter6 = Parameter6;
        }

        [Example("When all parameters specified by the parent fixture and some parameters are specified")]
        void Ex01()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
            Expect("the parameter3 should be the value specified by the parent", () => Parameter3 == true);
            Expect("the parameter4 should be the value specified by the parent", () => Parameter4 == 7.77);
            Expect("the parameter5 should be the value specified by the parent", () => Parameter5 == "Parameter5");
            Expect("the parameter6 should be the value specified by the parent", () => Parameter6 == 999);
        }

        [Example("When all parameters specified by the parent fixture and some parameters are specified")]
        void Ex02()
        {
            Expect("the parameter1 should be the value specified by the parent", () => Parameter1 == 777);
            Expect("the parameter2 should be the value specified by the parent", () => Parameter2 == "Parameter");
            Expect("the parameter3 should be the value specified by the parent", () => Parameter3 == true);
            Expect("the parameter4 should be the value specified by the parent", () => Parameter4 == 7.77);
            Expect("the parameter5 should be the value specified by the parent", () => Parameter5 == "Parameter5");
            Expect("the parameter6 should be the value specified by the parent", () => Parameter6 == 999);
        }
    }
}