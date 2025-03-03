import React, { useState, useEffect } from "react";
import { View, Text, TouchableOpacity, StyleSheet, Alert, ScrollView } from "react-native";
import { db } from "../src/firebaseConfig";
import { addDoc, collection } from "firebase/firestore";
import { Picker } from "@react-native-picker/picker";
import Footer from "../components/Footer";

const SugestaoServicosClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [servicos, setServicos] = useState<any[]>([]);
  const [motivoRecusa, setMotivoRecusa] = useState<string | undefined>(undefined);

  useEffect(() => {
    // Simulação do cenário que queremos para o projeto.
    const servicosData = [
      {
        id: "1",
        nome: "João",
        sobrenome: "Silva",
        cpf: "12345678900",
        dataNascimento: "01/01/1980",
        idade: "41",
        altura: "1.75m",
        data: "10/04/2025",
        turno: "Manhã"
      },
      {
        id: "2",
        nome: "Maria",
        sobrenome: "Oliveira",
        cpf: "98765432100",
        dataNascimento: "15/05/1990",
        idade: "30",
        altura: "1.65m",
        data: "22/04/2025",
        turno: "Manhã"
      },
      {
        id: "3",
        nome: "Carlos",
        sobrenome: "Pereira",
        cpf: "11223344556",
        dataNascimento: "10/10/1985",
        idade: "35",
        altura: "1.80m",
        data: "01/04/2025",
        turno: "Noite"
      },
    ];

    setServicos(servicosData); // Definir os dados de serviços
  }, []);

  const cadastrarServicoAceito = async (servico: any) => {
    try {
      // Referência para a coleção t_sugestao_consulta_clinica
      const servicosRef = collection(db, "t_sugestao_consulta_clinica");
      
      // Adicionando o serviço com status "aceito"
      await addDoc(servicosRef, {
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        status: "aceito", // Status "aceito"
        motivoRecusa: null, // No caso de aceitação, motivo é null
        data: servico.data,
        turno: servico.turno,
      });

      setServicos((prevState) => prevState.filter((item) => item.id !== servico.id)); // Remove o card da tela
      Alert.alert("Sucesso", "Serviço aceito com sucesso! A sugestão será enviada ao cliente.");
    } catch (error) {
      console.error("Erro ao cadastrar serviço:", error);
      Alert.alert("Erro", "Não foi possível cadastrar o serviço.");
    }
  };

  const cadastrarServicoRecusado = async (servico: any, motivo: string) => {
    try {
      // Referência para a coleção t_sugestao_consulta_clinica
      const servicosRef = collection(db, "t_sugestao_consulta_clinica");
      
      // Adicionando o serviço com status "recusado", vamos usar esta tabela em outra atividade.
      await addDoc(servicosRef, {
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        status: "recusado", // Status "recusado"
        motivoRecusa: motivo, // Motivo da recusa
        data: servico.data,
        turno: servico.turno,
      });

      // Adicionando o motivo da recusa na tabela t_motivo_recusa
      const motivosRef = collection(db, "t_servicos_recusados");
      await addDoc(motivosRef, {
        servicoId: servico.id,
        motivo: motivo,
        nome: servico.nome,
        sobrenome: servico.sobrenome,
        cpf: servico.cpf,
        dataNascimento: servico.dataNascimento,
        idade: servico.idade,
        altura: servico.altura,
        data: servico.data,
        turno: servico.turno,
      });

      setServicos((prevState) => prevState.filter((item) => item.id !== servico.id)); // Remove o card da tela
      Alert.alert("Sucesso", `Serviço recusado com sucesso! Motivo: ${motivo}`);
    } catch (error) {
      console.error("Erro ao cadastrar serviço:", error);
      Alert.alert("Erro", "Não foi possível cadastrar o serviço.");
    }
  };

  return (
    <ScrollView 
            contentContainerStyle={styles.scrollContainer} 
            keyboardShouldPersistTaps="handled"
          >
        <View style={styles.container}>
        <Text style={styles.title}>Serviços Disponíveis</Text>

        {servicos.map((servico) => (
            <View key={servico.id} style={styles.card}>
            <Text style={styles.cardText}>Nome: {servico.nome} {servico.sobrenome}</Text>
            <Text style={styles.cardText}>CPF: {servico.cpf}</Text>
            <Text style={styles.cardText}>Data de Nascimento: {servico.dataNascimento}</Text>
            <Text style={styles.cardText}>Idade: {servico.idade}</Text>
            <Text style={styles.cardText}>Altura: {servico.altura}</Text>
            <Text style={styles.cardText}>Data: {servico.data}</Text>
            <Text style={styles.cardText}>Turno: {servico.turno}</Text>

            <View style={styles.buttonContainer}>
                <TouchableOpacity
                style={[styles.button, { backgroundColor: "#4CAF50" }]}
                onPress={() => cadastrarServicoAceito(servico)}
                >
                <Text style={styles.buttonText}>Aceitar</Text>
                </TouchableOpacity>

                <TouchableOpacity
                style={[styles.button, { backgroundColor: "#f44336" }]}
                onPress={() => setMotivoRecusa(servico.id)}
                >
                <Text style={styles.buttonText}>Recusar</Text>
                </TouchableOpacity>
            </View>

            {motivoRecusa === servico.id && (
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
                    onPress={() => cadastrarServicoRecusado(servico, motivoRecusa || '')}
                >
                    <Text style={styles.buttonText}>Confirmar Recusa</Text>
                </TouchableOpacity>
                </View>
            )}
            
            </View>
        ))}

        </View>
        <Footer textColor='#000'/>
    </ScrollView>
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
    marginTop:30,
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
    paddingVertical: 10,
    borderRadius: 8,
    width: "48%",
    alignItems: "center",
  },
  buttonRecusa: {
    paddingVertical: 10,
    borderRadius: 8,
    width: "100%",
    alignItems: "center",
  },
  buttonText: {
    fontSize: 16,
    color: "#fff",
    fontWeight: "bold",
  },
  recusaContainer: {
    marginTop: 10,
    padding: 15,
    backgroundColor: "#f2f2f2",
    borderRadius: 8,
    height:"20%",
  },
  recusaText: {
    fontSize: 16,
    marginBottom: 10,
  },
  picker: {
    height: 50,
    width: "100%",
    backgroundColor: "#fff",
    borderRadius: 8,
    borderWidth: 1,
    borderColor: "#ccc",
  },
});

export default SugestaoServicosClinicaScreen;
