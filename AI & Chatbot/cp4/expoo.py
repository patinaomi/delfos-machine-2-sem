#Crie ao menos 10 classes para representar animais de um zoológico, utilizando os 4 pilares da orientação a objetos:
#abstração;
#encapsulação
# herança;
# polimorfismo
# As classes devem conter atributos e métodos coerentes com as características e comportamentos das espécies representadas; 

# Observações:
# Este exercício vale 2 pontos, para compor a nota do CP2;
# O CP2 valerá até 8 pontos;
# O exercício é individual e trabalhos com alto grau de similaridade serão ambos desconsiderados (nota 0);
# Scripts com auto grau de similaridade com resultados gerados por IA generativa também serão desconsiderados (nota 0);
# A entrega deve ser realizada em um único arquivo de script Python, com extensão ".py", capaz de executar sem erros pelo terminal (CMD).

# Classes: Mamíferos, Répteis, Peixes, Aves, Insetos, Anfíbios
class Animal:
    def __init__(self, _classificacao, _nomeCient, _apelido, _sexo, _local):
        self.classificacao = _classificacao
        self.nome_cient = _nomeCient
        self.apelido = _apelido
        self.__sexo = _sexo
        self.local = _local


classe Arara(Animal):
    def __init__(self, _classificacao, _nomeCientm _apelido, _sexo, _local):
        super().__init__("Aves

class Gerente(Funcionario):

    def __init__(self, nome, cpf, salario, senha, qtd_funcionarios):
        super().__init__(nome, cpf, salario)
        self._senha = senha
        self._qtd_funcionarios = qtd_funcionarios


