import React from 'react';
import { View, Text, Button, StyleSheet } from 'react-native';
import CustomButton from '../components/CustomButton';
import Footer from '../components/Footer';

const SucessoScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Cadastro Realizado!</Text>
      <CustomButton title="Voltar para a Home" onPress={() => navigation.navigate("Home")} width={'90%'} textColor='#fff'/>

      <Footer textColor='#fff'/>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
    backgroundColor: "#fff"
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    color: " #024059"
  },
});

export default SucessoScreen;
