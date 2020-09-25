import 'antd/dist/antd.css';
import 'react-chat-elements/dist/main.css';
import React, { useState, useEffect }  from 'react'
import { Layout, Menu } from 'antd';
import { UserOutlined, VideoCameraOutlined } from '@ant-design/icons';
import { Channel } from './Components/Channel/Channel';
import {
	BrowserRouter as Router,
	Switch,
	Route,
	Link
} from 'react-router-dom';
const { Header, Content, Sider } = Layout;
function BasicLayout(props) {
	const [channels, setChannel] = useState([
		{
		id:0,
		name: 'General',
		dataSource: [{
			position: 'left',
			type: 'text',
			text: 'Bienvendio al canal General.',
			date: new Date()
		}]
	},{
		id:1,
		name: 'Meetings',
		dataSource: [{
			position: 'left',
			type: 'text',
			text: 'Bienvendio al canal Meeting.',
			date: new Date()
		}]
	},
]);

useEffect(() => {  }, [channels])

const addMessage=(id, msg)=>{
	return setChannel(channels.map((channel)=>{
		if(channel.id===id)
			channel.dataSource.push(msg);
		return channel
	}))
}
	return (
		<Router>
			<Layout style={{ height: '100vh' }}>
				<Sider
					breakpoint='lg'
					collapsedWidth='0'
					
				>
					<div className='logo' />
					<Menu theme='dark' mode='inline' defaultSelectedKeys={['4']}>
						<Menu.Item key='1' icon={<UserOutlined />}>
							<Link to='/general'>General</Link>
						</Menu.Item>
						<Menu.Item key='2' icon={<VideoCameraOutlined />}>
							<Link to='/meetings'>Meetings</Link>
						</Menu.Item>
					</Menu>
				</Sider>
				<Layout>
					<Header className='site-layout-sub-header-background' style={{ padding: 0 }} />
					<Content style={{ margin: '24px 16px 0' }}>
						<Switch>
							{
								channels.map((channel)=>(
								<Route key={channel.id} path={`/${channel.name}`}>
								<Channel  addMessage={addMessage} dataSource={channel.dataSource} id={channel.id}/>
							</Route>))
							}
						</Switch>
					</Content>
				</Layout>
			</Layout>
		</Router>
	);
}

export default BasicLayout;
