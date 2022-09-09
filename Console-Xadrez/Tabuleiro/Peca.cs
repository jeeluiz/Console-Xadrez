namespace Tabuleiro
{
    public abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get;protected set; }
        public TabuleiroTab tab { get; protected set; }

        public Peca(Cor cor, TabuleiroTab tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.qteMovimentos = 0;
            this.tab = tab;
        }

        public void IncrementarQteMovimentos()
        {
            qteMovimentos++;
        }
        
        public bool ExisteMovimentoPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i=0; i<tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Linhas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
       
    }
}

