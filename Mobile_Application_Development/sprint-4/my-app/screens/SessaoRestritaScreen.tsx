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
      <Text style={styles.title}>Área Restrita</Text>

      <Text style={styles.title}>Olá {nome}, bem vindo!</Text>

      <CustomButton title="📋 Cadastrar Dados" onPress={() => navigation.navigate("DadoPessoal")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🔍 Consultar Dados" onPress={() => navigation.navigate("ConsultarDados")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="📅 Agendamentos" onPress={() => navigation.navigate("Agendamentos")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🚪 Sair" onPress={handleSignOut} backgroundColor="#D32F2F" textColor="#fff" width={'100%'}/>
      
      <Footer textColor="#000"/>
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
  },
});

export default SessaoRestritaScreen;
