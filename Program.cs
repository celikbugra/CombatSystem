if (args.Contains("--test"))
{
    TestRunner.RunAllTests();
    return;
}


// ==============================
// NORMAL PROGRAM
// ==============================

Geralt geralt = new Geralt();
Vampire vampire = new Vampire();

geralt.Attack(vampire);
vampire.Attack(geralt);