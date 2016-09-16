using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTest.IntegrationTestRunner
{
    public interface ITestRunnerCallback
    {
        void RunStarted(string platform, System.Collections.Generic.List<TestComponent> testsToRun);
        void RunFinished(System.Collections.Generic.List<TestResult> testResults);
        void AllScenesFinished();
        void TestStarted(TestResult test);
        void TestFinished(TestResult test);
        void TestRunInterrupted(System.Collections.Generic.List<ITestComponent> testsNotRun);
    }
}
