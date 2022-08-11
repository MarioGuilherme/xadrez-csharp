namespace TabuleiroNM {
    class Tabuleiro {
        public int Linha { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas { get; set; }

        public Tabuleiro(int linhas, int colunas) {
            Linha = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }
    }
}