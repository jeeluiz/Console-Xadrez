using Console_Xadrez;
using Tabuleiro;
using Xadrez;

try
{
    TabuleiroTab tab = new TabuleiroTab(8, 8);

    tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
    tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
    tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(0, 2));

    tab.colocarPeca(new Rei(Cor.Branca, tab), new Posicao(3,5));
    Tela.imprimirTabuleiro(tab);
}
catch (TabuleiroException e){
    Console.WriteLine(e.Message);
}
Console.ReadLine();
