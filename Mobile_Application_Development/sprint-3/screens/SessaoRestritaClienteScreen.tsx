import React, { useEffect, useState } from "react";
import { View, Text, TouchableOpacity, StyleSheet } from "react-native";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { auth, db } from "../src/firebaseConfig";
import { doc, getDoc } from "firebase/firestore";
import {signOut} from 'firebase/auth'

const SessaoRestritaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  
  const user = auth.currentUser;
  const [nome, setNome] = useState<string>("");
  
  useEffect(() => {
    if (user) {
      const fetchData = async () => {
        try {
          const userRef = doc(db, "t_dados_cadastrais", user.uid);
          const userSnap = await getDoc(userRef);

          if (userSnap.exists()) {
            const userData = userSnap.data();
            setNome(userData.nome || "Usuário");
          } else {
            setNome("Usuário");
          }
        } catch (error) {
          console.error("Erro ao buscar os dados:", error);
          setNome("Usuário");
        }
      };

      fetchData();
    }
  }, [user]);

  const handleSignOut = async () => {
    try {
      await signOut(auth);
      //navigation.navigate("Home");
      navigation.reset({
        index: 0,
        routes: [{ name: "Home" }]
      });
    } catch (error) {
      console.error("Erro ao sair:", error);
    }
  };

  return (
    <View style={styles.container}>

      <View>
        <Text style={styles.title}>✨ Seu Sorriso, Nossa Prioridade! ✨</Text>

        <Text style={styles.description}>Aceite a sugestão de suas consultas de forma inteligente, aproveite benefícios exclusivos e cuide da sua saúde bucal com praticidade e eficiência.</Text>
      </View>

      {/*}
      <View>
        <Text style={styles.titleName}>Olá {nome}!</Text>
      </View>*/}

      <View style={styles.sectionButton}>
        <CustomButton title="📋 Cadastrar Dados" onPress={() => navigation.navigate("CadastrarDadosPessoaisClientes")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="🔍 Consultar Dados" onPress={() => navigation.navigate("ConsultarDadosCliente")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="🏥 Sugestão de consultas" onPress={() => navigation.navigate("SugestaoServicosCliente")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="📅 Agendamentos" onPress={() => navigation.navigate("AgendamentosCliente")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="🏆 Sobre o Programa de benefícios" onPress={() => navigation.navigate("AtividadesProgramaBeneficioCliente")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="🍿 Conteúdos preventivos" onPress={() => navigation.navigate("ConteudoPreventivoCliente")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

        <CustomButton title="🚪 Sair" onPress={handleSignOut} backgroundColor="#ff5d4b" textColor="#fff" width={'100%'}/>
      </View>

      <Footer textColor="#024059"/>
    </View>
    
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    width: '100%'
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
    color:'#024059'
  },
  titleName: {
    fontSize: 20,
    fontWeight: "semibold",
    marginBottom: 20,
    color:'#ff5d4b'
  },
  description: {
    fontSize: 16,
    marginBottom: 20,
    color:'#024059'
  },
  sectionButton: {
    width: '100%',
    justifyContent: 'center',
    alignItems: 'center',
    marginVertical: 20,
  },
});

export default SessaoRestritaScreen;
