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
        return (f'....: ANIMAL #{self.id} :.....\nNome...: {self.nome}\nIdade..: {self.idade}\nCor....: {self.cor}\n'
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


# ----- PEIXES -----
class Peixe(Animal):
    def __init__(self, nome, idade, cor, sexo, status):
        super().__init__(nome, idade, cor, sexo, status)
        

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



# Instanciando animais
leao = Leao('Simba', 3, 'Begezinho', 'M', True, 'Longo')
gato = Gato('Bernadete', 1, 'Rajadinho', 'F', True, 'Curto', 'SRD')
macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')

#macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')
#macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')
#macaco = Macaco('Donkey', 6, 'Marrom', 'M', False, 'Curto')

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
