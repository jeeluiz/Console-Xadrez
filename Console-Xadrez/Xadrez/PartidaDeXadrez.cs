using Console_Xadrez.Xadrez;
using Tabuleiro;

namespace Xadrez
{
    public class PartidaDeXadrez
    {

        public TabuleiroTab tab { get; private set; }
        public int Turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new TabuleiroTab(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            //# jogadaespecial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.IncrementarQteMovimentos();
                tab.colocarPeca(T,destinoT);
            }
            //# jogadaespecial roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna -4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.IncrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            //# jogadaespecial em passant
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == null) 
                {
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);                     
                };
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem,Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            //# jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
            //# jogadaespecial roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna -4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            //# jogadaespecial en passant
            if( p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3,destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna); 
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se Colocar em Xeque!");
            }

            if (EstaEmXeque(adiversario(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequemate(adiversario(jogadorAtual)))
            {
                terminada = true;
            }

            Turno++;
            MudarJogador();
            Peca p = tab.peca(destino);

            // #jogadaespecial en passant
            if(p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
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
            if (!tab.peca(origem).MovimentosPossiveis(destino))
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Peca rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new TabuleiroException("Não tem rei da Cor" + cor + "no tabuleiro!");
            }
            foreach(Peca x in PecasEmJogo(adiversario(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
      
        private Cor adiversario(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor)) { return false; }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < tab.Linhas; i++)
                {
                    for(int j=0; j<tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem,destino );
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }    
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        
        private void colocarPecas()
        {
           ColocarNovaPeca('a', 1, new Torre(Cor.Branca,tab));
           ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
           ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
           ColocarNovaPeca('d', 1, new Dama(Cor.Branca, tab));
           ColocarNovaPeca('e', 1, new Rei(Cor.Branca, tab,this ));
           ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
           ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
           ColocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
           ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branca,this));
           ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branca,this));
           
           ColocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
           ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
           ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
           ColocarNovaPeca('d', 8, new Dama(Cor.Preta, tab));
           ColocarNovaPeca('e', 8, new Rei(Cor.Preta, tab,this));
           ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
           ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
           ColocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
           ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta,this));
           ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preta,this)); ;
        }
    }


}

