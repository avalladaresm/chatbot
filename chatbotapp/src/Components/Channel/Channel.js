import React, { useState }  from 'react'
import { MessageList } from 'react-chat-elements'
import { Input, Button } from 'antd'
import axios from 'axios';
export const Channel = (props) => {
    const { dataSource, addMessage, id} = props;
    const [message, setMessage] = useState();

    const createNewMessage = (message) => {
        let msg = { position: 'right', type: 'text', text: message, date: new Date() }
        addMessage(id, msg);
        setMessage('');
    }

    const createTrelloBoard=(Nombre)=>{
        axios.post(`https://api.trello.com/1/boards/?key=0edca4f81481665db4f3720fa8fdad4a&token=1807a1f4dca52d4ec812b53a814bea297e2389405b48a7a329ffeba9a16bd9a2&name=${Nombre}`);
    }

    const BotMessage = (message)=>{
        createNewMessage(message);
        const options={Utterance:message};
        axios.post("https://localhost:44307/api/ChatItems",options).then((response)=>{
            const data = response.data;
            const prediction = data.prediction;
            const {topIntent, entities}=prediction;
            switch(topIntent){
                case "Create Trello":createTrelloBoard(entities.Nombre);break;
                default: createNewMessage("No comprendi, que ocupas que haga?");
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