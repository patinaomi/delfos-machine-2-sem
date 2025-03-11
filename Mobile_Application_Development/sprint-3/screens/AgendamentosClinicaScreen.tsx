import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, Alert, StyleSheet, ScrollView } from 'react-native';
import { db, auth } from '../src/firebaseConfig'; // Firebase config
import { collection, query, where, getDocs, doc, getDoc } from 'firebase/firestore';
import CustomButton from '../components/CustomButton';
import { Linking } from 'react-native';

interface Agendamento {
  data: string;
  turno: string;
  idCliente: string;
  nomeClinica: string;
  latitude: number;
  longitude: number;
}

interface Cliente {
  nome: string;
  sobrenome: string;
  cpf: string;
  dataNascimento: string;
  idade: string;
  altura: string;
  cepResidencia: string;
  cidadeResidencia: string;
  estadoResidencia: string;
  ruaResidencia: string;
  numeroResidencia: string;
  cepPreferenciaCliente: string;
  cidadePreferenciaCliente: string;
  estadoPreferenciaCliente: string;
  ruaPreferenciaCliente: string;
  numeroPreferenciaCliente: string;
  diaPreferenciaCliente: string[];
  turnoPreferenciaCliente: string[];
}

const AgendamentosClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [agendamentos, setAgendamentos] = useState<{ agendamento: Agendamento; cliente: Cliente | null }[]>([]);

  const fetchAgendamentos = async () => {
    try {
      const clinicaId = auth.currentUser?.uid; // ID da clínica logada

      if (!clinicaId) {
        Alert.alert('Erro', 'Você precisa estar logado para ver os agendamentos.');
        return;
      }

      // Busca os agendamentos onde clinicaId == usuário logado e status == 'aceito'
      const q = query(
        collection(db, 't_sugestao_consulta_cliente'),
        where('clinicaId', '==', clinicaId),
        where('status', '==', 'aceito')
      );

      const querySnapshot = await getDocs(q);
      const agendamentosData: { agendamento: Agendamento; cliente: Cliente | null }[] = [];

      for (const docSnap of querySnapshot.docs) {
        const agendamento = docSnap.data() as Agendamento;

        // Busca os dados do cliente na outra coleção
        const clienteRef = doc(db, 't_dados_pessoais_clientes', agendamento.idCliente);
        const clienteSnap = await getDoc(clienteRef);

        let clienteData: Cliente | null = null;
        if (clienteSnap.exists()) {
          clienteData = clienteSnap.data() as Cliente;
        }

        agendamentosData.push({ agendamento, cliente: clienteData });
      }

      setAgendamentos(agendamentosData);
    } catch (error) {
      console.error('Erro ao buscar agendamentos:', error);
      Alert.alert('Erro', 'Falha ao carregar os agendamentos.');
    }
  };

  useEffect(() => {
    fetchAgendamentos();
  }, []);

  const openMap = (latitude: number, longitude: number) => {
    const url = `https://www.google.com/maps?q=${latitude},${longitude}`;
    Linking.openURL(url);
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Agendamentos Confirmados</Text>
      {agendamentos.length === 0 ? (
        <Text style={styles.noAppointments}>Nenhum agendamento encontrado.</Text>
      ) : (
        agendamentos.map(({ agendamento, cliente }, index) => (
          <View style={styles.card} key={index}>
            <Text style={styles.cardText}>📅 Data: {agendamento.data}</Text>
            <Text style={styles.cardText}>🌅 Turno: {agendamento.turno}</Text>
            
            {cliente && (
              <>
                <Text style={styles.cardText}>👤 Cliente: {cliente.nome} {cliente.sobrenome}</Text>
                <Text style={styles.cardText}>🆔 CPF: {cliente.cpf}</Text>
                <Text style={styles.cardText}>🎂 Data de Nascimento: {cliente.dataNascimento}</Text>
                <Text style={styles.cardText}>📏 Altura: {cliente.altura} cm</Text>
                <Text style={styles.cardText}>🏠 Endereço Residencial: {cliente.ruaResidencia}, {cliente.numeroResidencia}, {cliente.cidadeResidencia} - {cliente.estadoResidencia}</Text>
                <Text style={styles.cardText}>📍 Endereço de Consulta: {cliente.ruaPreferenciaCliente}, {cliente.numeroPreferenciaCliente}, {cliente.cidadePreferenciaCliente} - {cliente.estadoPreferenciaCliente}</Text>
                <Text style={styles.cardText}>📅 Dias Preferidos: {cliente.diaPreferenciaCliente.join(', ')}</Text>
                <Text style={styles.cardText}>🕒 Turno Preferido: {cliente.turnoPreferenciaCliente.join(', ')}</Text>
              </>
            )}

          </View>
        ))
      )}

      <CustomButton
        title="Voltar"
        onPress={() => navigation.goBack()}
        width="100%"
        textColor="#fff"
      />
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#f4f4f4',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#024059',
    marginBottom: 20,
  },
  noAppointments: {
    fontSize: 16,
    color: '#555',
    textAlign: 'center',
  },
  card: {
    backgroundColor: '#fff',
    padding: 15,
    marginBottom: 15,
    borderRadius: 10,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.3,
    shadowRadius: 5,
    elevation: 3,
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#0A4275',
    marginBottom: 10,
  },
  cardText: {
    fontSize: 16,
    color: '#555',
    marginBottom: 5,
  },
  locationButton: {
    marginTop: 15,
    paddingVertical: 10,
    backgroundColor: '#08c8f8',
    borderRadius: 8,
    alignItems: 'center',
  },
  locationButtonText: {
    color: '#fff',
    fontSize: 16,
  },
});

export default AgendamentosClinicaScreen;
