if (args.Contains("--test"))
{
    TestRunner.RunAllTests();
    return;
}


// * ============================== * //
// * NORMAL PROGRAM                 * //
// * ============================== * //

// Config.ShowImage("images/Vampire.png");

Geralt geralt = new Geralt();

Vampire vampire = new Vampire();

Console.WriteLine("geralt health " + geralt.Health);


vampire.Attack(geralt.Companion);
vampire.Attack(geralt.Companion);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);
vampire.Attack(geralt);

Console.WriteLine("geralt health " + geralt.Health);
Console.WriteLine("Companion health " + geralt.Companion.Health);



geralt.ShowItems();