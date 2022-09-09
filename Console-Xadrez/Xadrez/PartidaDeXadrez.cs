using Tabuleiro;

namespace Xadrez
{
    public class PartidaDeXadrez
    {

        public TabuleiroTab tab { get; private set; }
        public int Turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new TabuleiroTab(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Nao existe peça na posicao escolhida");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida nao é sua");
            }
            if (!tab.peca(pos).ExisteMovimentoPossiveis())
            {
                throw new TabuleiroException("Nao  ha movimentos possiveis para  a peça de origem escolhida");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida!");
            }
        }

        private void MudarJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 1).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 2).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('d', 2).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 2).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 1).ToPosicao());
            tab.colocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('d', 1).ToPosicao());

            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 7).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 8).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('d', 7).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 7).ToPosicao());
            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 8).ToPosicao());
            tab.colocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }


}

