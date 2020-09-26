import React, { useState, useEffect }  from 'react'
import { MessageList } from 'react-chat-elements'
import { Input, Button } from 'antd'
import axios from 'axios';

export const Channel = (props) => {
	const { dataSource, addMessage, id, boards} = props;
	const [message, setMessage] = useState();
	const [activeBoard, setActiveBoard] = useState();
	const [defaultList, setDefaultList] = useState('To Do');
	const [lists, setLists] = useState()
	const [cards, setCards] = useState()

	const createNewMessage = (message) => {
			let msg = { position: 'right', type: 'text', text: message, date: new Date() }
			addMessage(id, msg);
			setMessage('');
	}
	
	const createNewBotMessage = (message) => {
		let msg = { position: 'left', type: 'text', text: message, date: new Date() }
		addMessage(id, msg);
		setMessage('');
	}

	const createTrelloBoard=(Nombre)=>{
		const options={Name:Nombre};
		axios.post('http://localhost:8081/api/TrelloBoard', options).then((res) => {
			if(res.data)
				createNewBotMessage(`Tablero "${res.data.name}" creado!`);
				setActiveBoard(res.data.id)
				getTrelloLists(res.data.id)
		}).catch((e) => {
			createNewBotMessage(`Ha ocurrido un error creando el tablero :(`);
		});
	}
	
	const getTrelloLists = (boardId) => {
		axios.get(`http://localhost:8081/api/TrelloBoard/${boardId}/Lists`).then((res) => {
			console.log("data",res.data)
			setLists(res.data)
		})
	}

	const getTrelloCards = (boardId) => {
		axios.get(`http://localhost:8081/api/TrelloBoard/${boardId}/Cards`).then((res) => {
			setCards(res.data)
		})
	}
	
	const createTrelloCard=(data)=>{
		const targetList = lists.filter(x => {
			return x && x.name === defaultList ? x.id : ''
		})
		const options={Name:data.name, IdList: targetList[0].id, Desc: data.desc};
		axios.post('http://localhost:8081/api/TrelloCard', options).then((res) => {
			if(res.data){
				createNewBotMessage(`Tarjeta "${res.data.name}" creada en lista ${defaultList}!`);
				getTrelloCards(activeBoard)
			}
		}).catch((e) => {
			createNewBotMessage(`Ha ocurrido un error creando la tarjeta :(`);
		});
	}
		
	const moveTrelloCard=(Nombre, NombreColumna)=>{
		const targetList = lists.filter(x => {
			return x && x.name === NombreColumna ? x.id : ''
		})

		const targetCard = cards.filter(x => {
			return x && x.name === Nombre ? x.id : ''
		})
		if(!targetList || !targetList.length || !targetCard && !targetCard.length ) return createNewBotMessage(`La columna o la tarjeta no existe :(`);
		const column = targetList[0];
		const card = targetCard[0];
		const options={IdList: column.id, Desc: card.desc};
		
		axios.put(`http://localhost:8081/api/TrelloCard/${card.id}`, options).then((res) => {
			if(res.data){
				createNewBotMessage(`Tarjeta "${res.data.name}" movida a la columna ${column.name}!`);
			}
		}).catch((e) => {
			createNewBotMessage(`Ha ocurrido un error moviendo la tarjeta :(`);
		});
	}

	const modifyCard=(Nombre, descripcion)=>{
		const targetCard = cards.filter(x => {
			return x && x.name === Nombre ? x.id : ''
		})

		if(!targetCard && !targetCard.length ) return createNewBotMessage(`La columna o la tarjeta no existe :(`);
		const card = targetCard[0];
		const options={ Desc: descripcion};
		axios.put(`http://localhost:8081/api/TrelloCard/${card.id}`, options).then((res) => {
			if(res.data){
				createNewBotMessage(`Tarjeta "${res.data.name}" con nueva descripcion !`);
			}
		}).catch((e) => {
			createNewBotMessage(`Ha ocurrido un error moviendo la tarjeta :(`);
		});
	}

	const createMeeting = (dia, hora) => {
		const options={ Dia: dia, Hora: hora};
		axios.post(`http://localhost:8081/api/Meeting`, options).then((res) => {
			if(res.data){
				createNewBotMessage(`Reunion agendada para ${dia} con hora ${hora}!`);
			}
		}).catch((e) => {
			createNewBotMessage(`Ha ocurrido un error creando la reunión :(`);
		});
	}

	const BotMessage = (message)=>{
		createNewMessage(message);
		const options={Utterance:message};
		axios.post("http://localhost:8081/api/ChatItems",options).then((response)=>{
			const data = response.data;
			const prediction = data.prediction;
			const {topIntent, entities}=prediction;
			console.log(prediction)
			switch(topIntent){
				case "Create Trello":return createTrelloBoard(entities.Nombre.toString());	
				case "Create Card":return createTrelloCard({name: entities.Nombre.toString(), desc: entities.Descripcion && entities.Descripcion.toString() }); 
				case "Move Card":return moveTrelloCard(entities.Nombre.toString(), entities.NombreColumna.toString());
				case "Modify Card":return modifyCard(entities.Nombre.toString(), entities.Descripcion.toString());
				case "meeting":return createMeeting(entities.Dia.toString(), entities.Hora.toString());
				
				default: createNewBotMessage("No comprendi, que ocupas que haga?");
			}
		})
	}

	return (
		<>
			<MessageList
				className='message-list'
				lockable={true}
				toBottomHeight={'100%'}
				dataSource={dataSource}
			/>
			<Input value={message} onChange={e => setMessage(e.target.value)} addonAfter={<Button onClick={() => BotMessage(message)}>Send</Button>} />
		</>
	)
}