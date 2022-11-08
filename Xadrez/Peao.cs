using TabuleiroNM;

namespace Xadrez {
    class Peao : Peca {
        private PartidaDeXadrez PartidaDeXadrez;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partidaDeXadrez) : base(tabuleiro, cor) {
            PartidaDeXadrez = partidaDeXadrez;
        }

        public override string ToString() {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao posicao) {
            return Tabuleiro.Peca(posicao) == null;
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            if (Cor == Cor.Branca) {
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                // #Jogada Especial - En Passant
                if (Posicao.Linha == 3) {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == PartidaDeXadrez.VulneravelEnPassant)
                        movimentosPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;

                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == PartidaDeXadrez.VulneravelEnPassant)
                        movimentosPossiveis[direita.Linha - 1, direita.Coluna] = true;
                }
            } else {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

                // #Jogada Especial - En Passant
                if (Posicao.Linha == 4) {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == PartidaDeXadrez.VulneravelEnPassant)
                        movimentosPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;

                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == PartidaDeXadrez.VulneravelEnPassant)
                        movimentosPossiveis[direita.Linha + 1, direita.Coluna] = true;
                }
            }

            return movimentosPossiveis;
        }
    }
}