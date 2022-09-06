using Console_Xadrez;
using Tabuleiro;
using Xadrez;

try
{
    TabuleiroTab tab = new TabuleiroTab(8, 8);
    PartidaDeXadrez partida = new PartidaDeXadrez();
    while (!partida.terminada)
    {

        Console.Clear();
        Tela.ImprimirTabuleiro(partida.tab);

        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partida.executaMovimento(origem, destino);
    }

    Tela.ImprimirTabuleiro(tab);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();
