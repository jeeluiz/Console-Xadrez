using Tabuleiro;

namespace Xadrez
{
    public class PartidaDeXadrez
    {
       
        public TabuleiroTab tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new TabuleiroTab(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre( Cor.Branca,tab), new PosicaoXadrez('c', 1).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Branca,tab), new PosicaoXadrez('c', 2).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Branca,tab), new PosicaoXadrez('d', 2).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Branca,tab), new PosicaoXadrez('e', 2).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Branca,tab), new PosicaoXadrez('e', 1).ToPosicao());
            tab.colocarPeca(new Rei(Cor.Branca,tab), new PosicaoXadrez('d', 1).ToPosicao());

            tab.colocarPeca(new Torre( Cor.Preta, tab), new PosicaoXadrez('c', 7).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Preta, tab), new PosicaoXadrez('c', 8).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Preta, tab), new PosicaoXadrez('d', 7).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Preta, tab), new PosicaoXadrez('e', 7).ToPosicao());
            tab.colocarPeca(new Torre( Cor.Preta, tab), new PosicaoXadrez('e', 8).ToPosicao());
            tab.colocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }


}

