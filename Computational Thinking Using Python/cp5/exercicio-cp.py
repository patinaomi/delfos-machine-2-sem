import random


class Animal:
    ult_id = 0

    def __init__(self, _nome, _idade, _cor, _sexo, _status):
        Animal.ult_id += 1
        self.id = Animal.ult_id
        self.nome = _nome
        self.idade = _idade
        self.cor = _cor
        self.sexo = _sexo
        self.status = _status

    def emitir_som(self):
        pass

    def movimentar(self):
        pass

    def __str__(self):
        return (f'\n....: ANIMAL #{self.id} :.....\nNome...: {self.nome}\nIdade..: {self.idade}\nCor....: {self.cor}\n'
                f'Sexo...: {self.sexo}\nStatus.: {"Vivo" if self.status else "Morto"}\n')


# ----- MAMIFEROS -----
class Mamifero(Animal):
    def __init__(self, nome, idade, cor, sexo, status, _tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status)
        self.tipo_do_pelo = _tipo_do_pelo

    def amamentar(self):
        if self.sexo == 'F':
            if self.status:
                return 'Amamentando bebezinho!'
            else:
                return 'Ela já morreu :('
        else:
            return 'Machos não amamentam! hihi'

    def __str__(self):
        return super().__str__() + f'Tipo...: {self.__class__.__name__}\n'


class Leao(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)

    def emitir_som(self):
        if self.status:
            return 'RWAAAAAAAAAAR!'
        else:
            return self.nome + ' está morto.'

    def movimentar(self):
        if self.status:
            return self.nome + ' está caçando!'
        else:
            return self.nome + ' está morto.'


class Macaco(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)

    def emitir_som(self):
        if self.status:
            return 'URRRUUUU URUUUUUU!'
        else:
            return self.nome + ' já morreu.'

    def movimentar(self):
        if self.status:
            return self.nome + ' está trepando no galho!'
        else:
            return self.nome + ' já morreu.'


class Gato(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo, _raca):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)
        self.raca = _raca

    def emitir_som(self):
        if self.status:
            return 'NYAAAN NYAAAN =ˆ.ˆ='
        else:
            return self.nome + ' já morreu.'

    def movimentar(self):
        if self.status:
            return self.nome + ' está brincando com seu ratinho!'
        else:
            return self.nome + ' já morreu.'


class Golfinho(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)

    def emitir_som(self):
        if self.status:
            return 'AAAAAN AAAAN AAAAAAAN'
        else:
            return self.nome + ' está morto.'

    def movimentar(self):
        if self.status:
            return self.nome + ' está pulando no Sea Park!'
        else:
            return self.nome + ' está morto.'


# ----- PEIXES -----
class Peixe(Animal):
    def __init__(self, nome, idade, cor, sexo, status, _tipo_agua):
        super().__init__(nome, idade, cor, sexo, status)
        self.tipo_agua = _tipo_agua

    def movimentar(self):
        if self.status:
            return self.nome + ' está nadando!'
        else:
            return self.nome + ' já morreu :('

    def emitir_som(self):
        if self.status:
            return 'SPLAAASH SPLAAAAAH'
        else:
            return self.nome + ' já morreu.'

    def __str__(self):
        return super().__str__() + f'Tipo...: {self.__class__.__name__}\nLocal..: {self.tipo_agua}\n'


class Tubarao(Peixe):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua):
        super().__init__(nome, idade, cor, sexo, status, tipo_agua)

    def movimentar(self):
        if self.status:
            return self.nome + ' está atacando pessoas na praia!'
        else:
            return 'Ela já morreu :('


class Piranha(Peixe):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua):
        super().__init__(nome, idade, cor, sexo, status, tipo_agua)

    def movimentar(self):
        if self.status:
            return self.nome + ' está atacando seu marido!'
        else:
            return 'Ela já morreu, pois mexeu com homem casado :('

    def emitir_som(self):
        if self.status:
            return 'OII VIDAA, ME PAGA UM COMBO???? ;)'
        else:
            return self.nome + ' já morreu.'


class Baiacu(Peixe):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua, _inflado):
        super().__init__(nome, idade, cor, sexo, status, tipo_agua)
        self.inflado = _inflado

    def inflar(self):
        if self.inflado:
            return self.nome + ' já está inflado.'
        else:
            return self.nome + ' INFLOUUUUUUU!'


# --- REPTÉIS
class Reptil(Animal):
    def __init__(self, nome, idade, cor, sexo, status):
        super().__init__(nome, idade, cor, sexo, status)

    def movimentar(self):
        if self.status:
            return self.nome + ' está rastejando!'
        else:
            return self.nome + ' já morreu :('

    def emitir_som(self):
        if self.status:
            return 'GRRRRAAAAAAAWW'
        else:
            return self.nome + ' já morreu.'

    def __str__(self):
        return super().__str__() + f'Tipo...: {self.__class__.__name__}\n'


class Cobra(Reptil):
    def __init__(self, nome, idade, cor, sexo, status, _veneno):
        super().__init__(nome, idade, cor, sexo, status)
        self.veneno = _veneno

    def emitir_som(self):
        if self.status:
            return 'PSSSSS PSSSSSS'
        else:
            return self.nome + ' já morreu.'

    def __str__(self):
        return super().__str__() + f'Veneno.: {"Letal" if self.veneno else "Não Letal"}\n'


class Jacare(Reptil):
    def __init__(self, nome, idade, cor, sexo, status):
        super().__init__(nome, idade, cor, sexo, status)

    def atacar(self):
        if self.status:
            return self.nome + ' arrancou a perna de algum aluno!!'
        else:
            return self.nome + ' já morreu.'


class Camaleao(Reptil):
    def __init__(self, nome, idade, cor, sexo, status):
        super().__init__(nome, idade, cor, sexo, status)

    def movimentar(self):
        if self.status:
            return self.nome + ' está andando!'
        else:
            return self.nome + ' já morreu :('

    def mudar_cor(self):
        num = random.randrange(1, 5)
        if num == 1:
            nova_cor = 'Azul'
        elif num == 2:
            nova_cor = 'Laranja'
        elif num == 3:
            nova_cor = 'Roxo'
        else:
            nova_cor = 'Multi-Cor'

        self.cor = nova_cor
        return self.nome + ' mudou de cor para ' + self.cor


# Instanciando animais
leao = Leao('Simba', 3, 'Begezinho', 'M', True, 'Longo')
gato = Gato('Bernadete', 1, 'Rajadinho', 'F', True, 'Curto', 'SRD')
macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')
golfinho = Golfinho('Delphi', 4, 'Cinzinha', 'M', True, 'Não tem')
tubarao = Tubarao('Cleitinho', 10, 'Cinza', 'M', True, 'Água Salgada')
piranha = Piranha('Tati', 4, 'Verde Brilhante', 'F', True, 'Água Doce')
baiacu = Baiacu('Pipoka', 8, 'Marrom com Bege', 'M', True, 'Água Doce', False)
cobra = Cobra('Julinha', 2, 'Laranjinha', 'F', True, True)
jacare = Jacare('Waguininho', 35, 'Marrom', 'M', True)
camaleao = Camaleao('Tharso', 3, 'Verde', 'M', True)

# Animal 1
print(leao)
print(leao.emitir_som())
print(leao.amamentar())
print(leao.movimentar())

# Animal 2
print(gato)
print(gato.emitir_som())
print(gato.amamentar())
print(gato.movimentar())

# Animal 3
print(macaco)
print(macaco.emitir_som())
print(macaco.amamentar())
print(macaco.movimentar())

# Animal 4
print(golfinho)
print(golfinho.emitir_som())
print(golfinho.amamentar())
print(golfinho.movimentar())

# Animal 5
print(tubarao)
print(tubarao.emitir_som())
print(tubarao.movimentar())

# Animal 6
print(piranha)
print(piranha.movimentar())
print(piranha.emitir_som())

# Animal 7
print(baiacu)
print(baiacu.movimentar())
print(baiacu.emitir_som())
print(baiacu.inflar())

# Animal 8
print(cobra)
print(cobra.movimentar())
print(cobra.emitir_som())

# Animal 9
print(jacare)
print(jacare.movimentar())
print(jacare.emitir_som())
print(jacare.atacar())

# Animal 10
print(camaleao)
print(camaleao.movimentar())
print(camaleao.emitir_som())
print(camaleao.mudar_cor())
