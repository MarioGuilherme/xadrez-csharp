using TabuleiroNM;

namespace Xadrez {
    class Rei : Peca {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {}

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            // Acima
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Nordeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Direita
            posicao.DefinirValores(posicao.Linha, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Abaixo
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudoeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Esquerda
            posicao.DefinirValores(posicao.Linha, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Noroeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            return movimentosPossiveis;
        }
    }
}