import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, Alert, StyleSheet, ScrollView } from 'react-native';
import { db, auth } from '../src/firebaseConfig';
import { collection, doc, getDoc, getDocs, query, where } from 'firebase/firestore';
import CustomButton from '../components/CustomButton';
import { Linking } from 'react-native';

interface Agendamento {
  data: string;
  turno: string;
  clinicaId: string;
}

interface Clinica {
  nome: string;
  cnpj: string;
  endereco: string;
  latitude?: number;
  longitude?: number;
  especialidades: string[];
}

const AgendamentosClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [agendamentos, setAgendamentos] = useState<Agendamento[]>([]);
  const [clinicas, setClinicas] = useState<{ [key: string]: Clinica }>({});

  // Buscar os agendamentos do cliente logado
  const fetchAgendamentos = async () => {
    try {
      const clienteId = auth.currentUser?.uid;

      if (!clienteId) {
        Alert.alert('Erro', 'Você precisa estar logado para ver seus agendamentos.');
        return;
      }

      const q = query(
        collection(db, 't_sugestao_consulta_cliente'),
        where('idCliente', '==', clienteId),
        where('status', '==', 'aceito')
      );

      const querySnapshot = await getDocs(q);
      const agendamentosData: Agendamento[] = [];

      querySnapshot.forEach((doc) => {
        agendamentosData.push(doc.data() as Agendamento);
      });

      if (agendamentosData.length === 0) {
        Alert.alert('Erro', 'Nenhum agendamento encontrado.');
        return;
      }

      setAgendamentos(agendamentosData);
      fetchClinicas(agendamentosData);
    } catch (error) {
      console.error('Erro ao buscar agendamentos:', error);
      Alert.alert('Erro', 'Falha ao carregar os agendamentos.');
    }
  };

  // Buscar os dados das clínicas associadas aos agendamentos
  const fetchClinicas = async (agendamentos: Agendamento[]) => {
    try {
      const clinicaData: { [key: string]: Clinica } = {};

      for (const agendamento of agendamentos) {
        if (!clinicaData[agendamento.clinicaId]) {
          const clinicaRef = doc(db, 't_dados_cadastrais_clinicas', agendamento.clinicaId);
          const clinicaSnap = await getDoc(clinicaRef);

          if (clinicaSnap.exists()) {
            const clinicaInfo = clinicaSnap.data() as { nome: string; cnpj: string };

            // Buscar endereço da clínica
            const enderecoRef = doc(db, 't_endereco_clinica', agendamento.clinicaId);
            const enderecoSnap = await getDoc(enderecoRef);
            let endereco = 'Endereço não encontrado';

            if (enderecoSnap.exists()) {
              const endData = enderecoSnap.data();
              endereco = `${endData.rua}, ${endData.numero} - ${endData.bairro}, ${endData.cidade} - ${endData.estado}, ${endData.cep}`;
            }

            // Buscar especialidades da clínica
            const especialidadesQuery = query(
              collection(db, 't_especialidades'),
              where('idClinica', '==', agendamento.clinicaId)
            );
            const especialidadesSnap = await getDocs(especialidadesQuery);
            const especialidades: string[] = especialidadesSnap.docs.map(doc => doc.data().nome);

            // Salvar os dados combinados
            clinicaData[agendamento.clinicaId] = {
              nome: clinicaInfo.nome,
              cnpj: clinicaInfo.cnpj,
              endereco,
              especialidades
            };
          }
        }
      }
      setClinicas(clinicaData);
    } catch (error) {
      console.error('Erro ao buscar dados da clínica:', error);
    }
  };

  useEffect(() => {
    fetchAgendamentos();
  }, []);

  const openMap = (latitude?: number, longitude?: number) => {
    if (latitude && longitude) {
      const url = `https://www.google.com/maps?q=${latitude},${longitude}`;
      Linking.openURL(url);
    } else {
      Alert.alert('Erro', 'Localização não disponível para esta clínica.');
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Meus Agendamentos</Text>
      {agendamentos.length === 0 ? (
        <Text style={styles.noAppointments}>Você não tem agendamentos.</Text>
      ) : (
        agendamentos.map((agendamento, index) => {
          const clinica = clinicas[agendamento.clinicaId];

          return (
            <View style={styles.card} key={index}>
              {clinica ? (
                <>
                  <Text style={styles.titleCard}>Dados da Clínica</Text>
                  <Text style={styles.cardText}>Nome: {clinica.nome}</Text>
                  <Text style={styles.cardText}>CNPJ: {clinica.cnpj}</Text>

                  <Text style={styles.titleCard}>Dados da Consulta</Text>
                  <Text style={styles.cardText}>Data: {agendamento.data}</Text>
                  <Text style={styles.cardText}>Turno: {agendamento.turno}</Text>

                  <Text style={styles.titleCard}>Endereço</Text>
                  <Text style={styles.cardText}>{clinica.endereco}</Text>

                  <Text style={styles.titleCard}>Especialidades</Text>
                  <Text style={styles.cardText}>{clinica.especialidades.join(', ') || 'Não informado'}</Text>

                  <TouchableOpacity
                    style={styles.locationButton}
                    onPress={() => openMap(clinica.latitude, clinica.longitude)}
                  >
                    <Text style={styles.locationButtonText}>Ver localização</Text>
                  </TouchableOpacity>
                </>
              ) : (
                <Text style={styles.cardText}>Carregando dados da clínica...</Text>
              )}
            </View>
          );
        })
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
  titleCard: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#0A4275',
    marginBottom: 10,
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
    paddingTop: 5
  },
  cardText: {
    fontSize: 16,
    color: '#555',
    marginBottom: 10,
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

export default AgendamentosClienteScreen;
