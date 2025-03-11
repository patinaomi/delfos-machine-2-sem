import React, { useState, useEffect } from "react";
import { View, Text, TouchableOpacity, StyleSheet, Alert, FlatList } from "react-native";
import { db } from "../src/firebaseConfig";
import { addDoc, collection } from "firebase/firestore";
import { Picker } from "@react-native-picker/picker";
import Footer from "../components/Footer";
import { getAuth } from "firebase/auth";

const SugestaoServicosClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [servicos, setServicos] = useState<any[]>([]);
  const [motivoRecusa, setMotivoRecusa] = useState<string | undefined>(undefined);
  const [servicoRecusado, setServicoRecusado] = useState<any | null>(null);

  useEffect(() => {
    const servicosData = [
      {
        idCliente: "V8yrYbJ60UWnsEHXZEee8qVErB13",
        nome: "Any",
        sobrenome: "Silva",
        cpf: "12345678900",
        dataNascimento: "01/01/1993",
        idade: "41",
        altura: "1.75m",
        data: "10/04/2025",
        turno: "Manhã",
      },
      {
        idCliente: "57x9ztnBO7QqRmbNB05uzucuLXK2",
        nome: "Caio",
        sobrenome: "Oliveira",
        cpf: "98765432100",
        dataNascimento: "15/05/1990",
        idade: "30",
        altura: "1.65m",
        data: "22/04/2025",
        turno: "Manhã",
      },
      {
        idCliente: "iAfMLw14Y1UnOsNFDvrd11FBRYq1",
        nome: "Claudio",
        sobrenome: "Pereira",
        cpf: "11223344556",
        dataNascimento: "10/10/1985",
        idade: "35",
        altura: "1.80m",
        data: "01/04/2025",
        turno: "Noite",
      },
    ];

    setServicos(servicosData);
  }, []);

  const cadastrarServicoAceito = async (servico: any) => {
    try {
      const auth = getAuth();
      const clinicaId = auth.currentUser?.uid;

      if (!clinicaId) {
        Alert.alert("Erro", "ID da clínica não encontrado.");
        return;
      }

      const servicosRef = collection(db, "t_sugestao_consulta_clinica");
      await addDoc(servicosRef, {
        idCliente: servico.idCliente,
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        status: "aceito",
        motivoRecusa: null,
        data: servico.data,
        turno: servico.turno,
        clinicaId: clinicaId,
      });

      setServicos((prevState) => prevState.filter((item) => item.idCliente !== servico.idCliente));
      Alert.alert("Sucesso", "Serviço aceito com sucesso!");
    } catch (error) {
      console.error("Erro ao cadastrar serviço:", error);
      Alert.alert("Erro", "Não foi possível cadastrar o serviço.");
    }
  };

  const cadastrarServicoRecusado = async (servico: any, motivo: string) => {
    try {
      const auth = getAuth();
      const clinicaId = auth.currentUser?.uid;

      if (!clinicaId) {
        Alert.alert("Erro", "ID da clínica não encontrado.");
        return;
      }

      const servicosRef = collection(db, "t_sugestao_consulta_clinica");
      await addDoc(servicosRef, {
        idCliente: servico.idCliente,
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        status: "recusado",
        motivoRecusa: motivo,
        data: servico.data,
        turno: servico.turno,
        clinicaId: clinicaId,
      });

      const motivosRef = collection(db, "t_servicos_recusados");
      await addDoc(motivosRef, {
        idCliente: servico.idCliente,
        servicoId: servico.idCliente, // Considerando o idCliente como identificador único do serviço
        motivo: motivo,
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        data: servico.data,
        turno: servico.turno,
        clinicaId: clinicaId,
      });

      setServicos((prevState) => prevState.filter((item) => item.idCliente !== servico.idCliente));
      Alert.alert("Sucesso", `Serviço recusado com sucesso! Motivo: ${motivo}`);
    } catch (error) {
      console.error("Erro ao cadastrar serviço:", error);
      Alert.alert("Erro", "Não foi possível cadastrar o serviço.");
    }
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={styles.card}>
      <Text style={styles.cardText}>Nome: {item.nome} {item.sobrenome}</Text>
      <Text style={styles.cardText}>CPF: {item.cpf}</Text>
      <Text style={styles.cardText}>Data de Nascimento: {item.dataNascimento}</Text>
      <Text style={styles.cardText}>Idade: {item.idade}</Text>
      <Text style={styles.cardText}>Altura: {item.altura}</Text>
      <Text style={styles.cardText}>Data: {item.data}</Text>
      <Text style={styles.cardText}>Turno: {item.turno}</Text>

      <View style={styles.buttonContainer}>
        <TouchableOpacity
          style={[styles.button, { backgroundColor: "#024059" }]}
          onPress={() => cadastrarServicoAceito(item)}
        >
          <Text style={styles.buttonText}>Aceitar</Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={[styles.button, { backgroundColor: "#ff5d4b" }]}
          onPress={() => setServicoRecusado(item)}
        >
          <Text style={styles.buttonText}>Recusar</Text>
        </TouchableOpacity>
      </View>

      {servicoRecusado?.idCliente === item.idCliente && (
        <View style={styles.recusaContainer}>
          <Text style={styles.recusaText}>Escolha o motivo da recusa:</Text>
          <Picker
            selectedValue={motivoRecusa}
            onValueChange={(itemValue) => setMotivoRecusa(itemValue)}
            style={styles.picker}
          >
            <Picker.Item label="Sem Médico" value="semMedico" />
            <Picker.Item label="Sem Agenda" value="semAgenda" />
            <Picker.Item label="Especialidade Não Disponível" value="especialidadeIndisponivel" />
          </Picker>

          <TouchableOpacity
            style={[styles.buttonRecusa, { backgroundColor: "#f44336" }]}
            onPress={() => cadastrarServicoRecusado(item, motivoRecusa || '')}
          >
            <Text style={styles.buttonText}>Confirmar Recusa</Text>
          </TouchableOpacity>
        </View>
      )}
    </View>
  );

  return (
    <FlatList
      data={servicos}
      renderItem={renderItem}
      keyExtractor={(item) => item.idCliente}
      contentContainerStyle={styles.scrollContainer}
    />
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: "#f9f9f9",
    minHeight: "110%",
    marginBottom: 30,
  },
  scrollContainer: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
    paddingHorizontal: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    textAlign: "left",
    marginBottom: 20,
    marginTop: 30,
    color: '#024059',
  },
  card: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.8,
    shadowRadius: 2,
    justifyContent: "space-between",
    marginTop: 30
  },
  cardText: {
    fontSize: 16,
    marginBottom: 5,
  },
  buttonContainer: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginTop: 10,
  },
  button: {
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 5,
    width: "45%",
    justifyContent: "center",
    alignItems: "center",
  },
  buttonText: {
    fontSize: 16,
    color: "#fff",
  },
  recusaContainer: {
    marginTop: 15,
  },
  recusaText: {
    fontSize: 16,
    marginBottom: 10,
  },
  picker: {
    width: "100%",
    height: 150,
  },
  buttonRecusa: {
    paddingVertical: 10,
    paddingHorizontal: 12,
    borderRadius: 5,
    marginTop: 10,
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
  },
});

export default SugestaoServicosClinicaScreen;
