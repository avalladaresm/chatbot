import React, { useState, useEffect } from 'react'
import { MessageList } from 'react-chat-elements'
import { Input, Button } from 'antd'

export const MeetingsChannel = () => {
  const [message, setMessage] = useState();
  const [dataSource, setDataSoure] = useState([{
    position: 'right',
    type: 'text',
    text: 'Lorem ipsum dolor sit amet, consectasdasdassdetur adipisicing elit',
    date: new Date(),
  },
  {
    position: 'right',
    type: 'text',
    text: 'Lorem ipsum dolor sit amet, consecasdasdastetur adipisicing elit',
    date: new Date(),
  },
  {
    position: 'left',
    type: 'text',
    text: 'Lorem ipsum dolor sit amet, consectasdasdsadetur adipisicing elit',
    date: new Date(),
  },
  {
    position: 'right',
    type: 'text',
    text: 'Lorem ipsum dolor sit amet, consecsadasdasdasdtetur adipisicing elit',
    date: new Date(),
  }]);
  
  useEffect(() => {
    

  }, [dataSource])
  
  const createNewMessage = (message) => {
    let m = {position:'right', type:'text', text:message, date:new Date()}
    setDataSoure([...dataSource, m]);
    setMessage('')
	}
	
  return (
    <>
      <MessageList
        className='message-list'
        lockable={true}
        toBottomHeight={'100%'}
        dataSource={dataSource}
      />
      <Input value={message} onChange={e => setMessage(e.target.value)} addonAfter={<Button onClick={() => createNewMessage(message)}>Send</Button>} />
    </>
  )
}