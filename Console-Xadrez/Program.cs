using Console_Xadrez;
using Tabuleiro;
using Xadrez;

TabuleiroTab tab = new TabuleiroTab(8, 8);

tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

Tela.imprimirTabuleiro(tab);

Console.ReadLine();
