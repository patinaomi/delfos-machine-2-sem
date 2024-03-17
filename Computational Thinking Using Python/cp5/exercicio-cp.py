class Animal:
    ult_id = 0

    def __init__(self, nome, idade, cor, sexo, status):
        Animal.ult_id += 1
        self.id = Animal.ult_id
        self.nome = nome
        self.idade = idade
        self.cor = cor
        self.sexo = sexo
        self.status = status

    def emitir_som(self):
        pass

    def movimentar(self):
        pass

    def __str__(self):
        return (f'\n....: ANIMAL #{self.id} :.....\nNome...: {self.nome}\nIdade..: {self.idade}\nCor....: {self.cor}\n'
                f'Sexo...: {self.sexo}\nStatus.: {"Vivo" if self.status else "Morto"}\n')


# ----- MAMIFEROS -----
class Mamifero(Animal):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status)
        self.tipo_do_pelo = tipo_do_pelo

    def amamentar(self):
        if self.sexo == 'F':
            if self.status:
                return 'Amamentando bebêzinho!'
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
            return self.nome + 'está morto'

    def movimentar(self):
        if self.status:
            return self.nome + ' está caçando!'
        else:
            return self.nome + 'está morto'


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
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo, raca):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)
        self.raca = raca

    def emitir_som(self):
        if self.status:
            return 'NYAAAN NYAAAN =ˆ.ˆ='
        else:
            return self.nome + 'já morreu.'

    def movimentar(self):
        if self.status:
            return self.nome + ' está brincando!'
        else:
            return self.nome + 'já morreu.'


class Leao(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)

    def emitir_som(self):
        if self.status:
            return 'RWAAAAAAAAAAR!'
        else:
            return self.nome + 'está morto'

    def movimentar(self):
        if self.status:
            return self.nome + ' está caçando!'
        else:
            return self.nome + 'está morto'


class Golfinho(Mamifero):
    def __init__(self, nome, idade, cor, sexo, status, tipo_do_pelo):
        super().__init__(nome, idade, cor, sexo, status, tipo_do_pelo)

    def emitir_som(self):
        if self.status:
            return 'AAAAAN AAAAN AAAAAAAN'
        else:
            return self.nome + 'está morto'

    def movimentar(self):
        if self.status:
            return self.nome + ' está pulando no Sea Park!'
        else:
            return self.nome + 'está morto'


# ----- PEIXES -----
class Peixe(Animal):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua):
        super().__init__(nome, idade, cor, sexo, status)
        self.tipo_agua = tipo_agua

    def movimentar(self):
        if self.status:
            return self.nome + ' está nadando!'
        else:
            return self.nome + ' já morreu :('

    def emitir_som(self):
        if self.status:
            return 'SPLAAASH SPLAAAAAH'
        else:
            return self.nome + 'já morreu.'

    def __str__(self):
        return super().__str__() + f'Tipo...: {self.__class__.__name__}\nLocal:...: {self.tipo_agua}\n'


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
            return self.nome + 'já morreu.'


class Baiacu(Peixe):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua, inflado):
        super().__init__(nome, idade, cor, sexo, status, tipo_agua)
        self.inflado = inflado

    def inflar(self):
        if self.inflado:
            return self.nome + ' já está inflado'
        else:
            return self.nome + ' INFLOUUUUUUU!'


# 
class Peixe(Animal):
    def __init__(self, nome, idade, cor, sexo, status, tipo_agua):
        super().__init__(nome, idade, cor, sexo, status)
        self.tipo_agua = tipo_agua

    def movimentar(self):
        if self.status:
            return self.nome + ' está nadando!'
        else:
            return self.nome + ' já morreu :('

    def emitir_som(self):
        if self.status:
            return 'SPLAAASH SPLAAAAAH'
        else:
            return self.nome + 'já morreu.'

    def __str__(self):
        return super().__str__() + f'Tipo...: {self.__class__.__name__}\nLocal:...: {self.tipo_agua}\n'




# Instanciando animais
leao = Leao('Simba', 3, 'Begezinho', 'M', True, 'Longo')
gato = Gato('Bernadete', 1, 'Rajadinho', 'F', True, 'Curto', 'SRD')
macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')
golfinho = Golfinho('Delphi', 4, 'Cinzinha', 'M', True, 'Não têm')
tubarao = Tubarao('Cleitinho', 10, 'Cinza', 'M', True, 'Água Salgada')
piranha = Piranha('LeidyEllin', 4, 'Verde Brilhante', 'F', True, 'Água Doce')
baiacu = Baiacu('Pipoka', 8, 'Marrom com Bege', 'M', True, 'Água Doce', False)

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
