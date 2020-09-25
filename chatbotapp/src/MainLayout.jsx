import 'antd/dist/antd.css';
import 'react-chat-elements/dist/main.css';
import React, { useEffect, useState } from 'react'
import { Layout, Menu, Result } from 'antd';
import { UploadOutlined, UserOutlined, VideoCameraOutlined } from '@ant-design/icons';

import {TrelloChannel} from './TrelloChannel'
import {MeetingsChannel} from './MeetingsChannel'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from 'react-router-dom';
import Cookie from 'js-cookie'
const { Header, Content, Sider } = Layout;

function BasicLayout(props) {
	const [authorized, setAuthorized] = useState(false)
	useEffect(() => {
		var referrer = document.referrer;
		referrer && referrer.length > 0 ? setAuthorized(true) : setAuthorized(false)
		console.log(referrer)
	}, [])

  return (
		<>
		{authorized ? <Router>
			<Layout style={{height:'100vh'}}>
				<Sider
					breakpoint='lg'
					collapsedWidth='0'
					onBreakpoint={broken => {
						console.log(broken);
					}}
					onCollapse={(collapsed, type) => {
						console.log(collapsed, type);
					}}
				>
					<div className='logo' />
					<Menu theme='dark' mode='inline' defaultSelectedKeys={['4']}>
						<Menu.Item key='1' icon={<UserOutlined />}>
							<Link to='/trello'>Trello chat</Link>
						</Menu.Item>
						<Menu.Item key='2' icon={<VideoCameraOutlined />}>
							<Link to='/meetings'>Meetings chat</Link>
						</Menu.Item>
					</Menu>
				</Sider>
				<Layout>
					<Header className='site-layout-sub-header-background' style={{ padding: 0 }} />
					<Content style={{ margin: '24px 16px 0' }}>
						<Switch>
							<Route path='/trello'>
								<TrelloChannel />
							</Route>
							<Route path='/meetings'>
								<MeetingsChannel />
							</Route>
						</Switch>
					</Content>
				</Layout>
			</Layout>
		</Router> 
		: 
		<Result title='403 FORBIDDEN' />}
		</>
		
  );
}

export default BasicLayout;
