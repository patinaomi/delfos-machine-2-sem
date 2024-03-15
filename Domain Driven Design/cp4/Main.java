package cp4;

/**
 * Inicia a aplicação da Panificadora Fiapão.
 * Esta classe contém o método {@code main}, que serve como ponto de entrada
 * para iniciar a interface de menu e permitir a interação do usuário com o
 * sistema de gerenciamento de produtos e vendas da panificadora.
 *
 * @author Patricia Naomi, Igor Gabriel
 * @version 1.0
 */

public class Main {
    public static void main(String[] args) {
        Menu menu = new Menu();
        menu.executarMenu();
    }
}