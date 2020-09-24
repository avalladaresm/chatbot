import 'antd/dist/antd.css';
import 'react-chat-elements/dist/main.css';
import React from 'react'
import { Layout, Menu } from 'antd';
import {  UserOutlined, VideoCameraOutlined } from '@ant-design/icons';

import { TrelloChannel } from './TrelloChannel'
import { MeetingsChannel } from './MeetingsChannel'
import {
	BrowserRouter as Router,
	Switch,
	Route,
	Link
} from 'react-router-dom';
const { Header, Content, Sider } = Layout;
function BasicLayout(props) {
	return (
		<Router>
			<Layout style={{ height: '100vh' }}>
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
	);
}

export default BasicLayout;
