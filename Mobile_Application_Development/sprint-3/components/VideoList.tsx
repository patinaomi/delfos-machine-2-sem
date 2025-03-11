// components/VideoList.tsx
import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image } from 'react-native';
import { ScrollView } from 'react-native-gesture-handler';
import CustomButton from './CustomButton';

const VideoCard = ({ title, image }: { title: string, image: any }) => {
  return (
    <View style={styles.card}>
      <Image 
        style={styles.image} 
        source={image}  // Agora 'image' pode ser um require
      />
      <Text style={styles.title}>{title}</Text>
    </View>
  );
};

//const VideoList = () => {: React.FC<{ navigation: any }> = ({ navigation }) => {
  const VideoList : React.FC<{ navigation: any }> = ({ navigation }) => {
  const videoData = [
    {
      title: 'Vídeo 1 - Introdução',
      image: require('../assets/Videos/introducao.jpg')
    },
    {
      title: 'Vídeo 2 - Primeiros Passos',
      image: require('../assets/Videos/primeiro-passos.jpg')
    },
    {
      title: 'Vídeo 3 - Como escovar os dentes',
      image: require('../assets/Videos/escovar-dentes.jpg')
    },
    {
      title: 'Vídeo 4 - Limpeza caseira',
      image: require('../assets/Videos/limpeza-caseira.jpg')
    },
    {
      title: 'Vídeo 5 - Sintomas suspeitos',
      image: require('../assets/Videos/sintomas.jpg')
    }
  ];

  return (
    <ScrollView>
      <View style={styles.container}>
        <Text style={styles.header}>Assista aos Vídeos e Ganhe Recompensas Exclusivas!</Text>
        
        <Text style={styles.description}>👀 Preparado para aprender e ganhar recompensas?</Text>
        
        <Text style={styles.description}>Aqui está a sua chance de acumular pontos valiosos enquanto aprende mais sobre nosso sistema e como aproveitar ao máximo os benefícios. São 5 vídeos exclusivos que você precisa assistir, e o melhor de tudo: cada vídeo assistido te aproxima de uma recompensa especial!</Text>
        
        <Text style={styles.header}>🎁 Como Funciona:</Text>

        <Text style={styles.headerSub}>1️⃣ Assista aos vídeos</Text>

        <Text style={styles.description}>
          São vídeos curtos e informativos que irão te guiar no uso completo da nossa plataforma.
        </Text>

        <Text style={styles.headerSub}>2️⃣ Ganhe pontos:</Text>

        <Text style={styles.description}>
          Cada vídeo completado conta pontos que você pode acumular.
        </Text>

        <Text style={styles.headerSub}>3️⃣ Troque por recompensas</Text>

        <Text style={styles.description}>
         Ao atingir marcos de pontos, você poderá resgatar prêmios incríveis e benefícios exclusivos!
        </Text>

        <Text style={styles.description}>🔥 Aproveite ao máximo e comece agora!</Text>

        <Text style={styles.description}>O conhecimento adquirido pode trazer ótimos benefícios, e suas recompensas estão a apenas alguns cliques de distância. Não perca essa oportunidade de aprender e conquistar!</Text>

        {videoData.map((video, index) => (
            <TouchableOpacity key={index} style={styles.cardButton}>
              <VideoCard title={video.title} image={video.image} />
            </TouchableOpacity>
        ))}

      <View style={styles.buttonContainer}>
        <CustomButton title="Conheça as Recompensas" onPress={() => navigation.navigate("ProgramaBeneficioCliente")} width={"100%"} textColor="#fff" />
      </View>

      <View style={styles.buttonContainer}>
        <CustomButton title="Home" onPress={() => navigation.navigate("SessaoRestritaCliente")} width={"100%"} textColor="#fff" backgroundColor="#f28705"/>
      </View>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
  },
  header: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    color:'#024059'
  },
  headerSub: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 10,
    color:'#024059'
  },
  description: {
    fontSize: 16,
    marginBottom: 20,
    color:'#024059'
  },
  cardButton: {
    marginBottom: 15,
  },
  card: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 10,
    alignItems: 'center',
  },
  image: {
    width: 340,
    height: 200,
    marginBottom: 10,
    borderRadius: 10,
  },
  title: {
    fontSize: 16,
    textAlign: 'center',
    color:'#024059'
  },
  buttonContainer: {
    width: "100%",
    alignItems: "center",
    marginBottom:0,
    marginTop:10,
  },
});

export default VideoList;
