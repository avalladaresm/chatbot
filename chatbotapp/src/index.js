import React, {useState, useEffect} from 'react';
import ReactDOM from 'react-dom';
import { Card, Form, Input, Button, message } from 'antd';
import './index.css';
import MainLayout from './MainLayout'
import Cookie from 'js-cookie'

const layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
const tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

const App = () => {
	const [user, setUser] = useState({})
	const [match, setMatch] = useState(false)
	const users = [{
		username: 'admin',
		password: 'pass'
	},
	{
		username: 'jose',
		password: 'pass'
	}]

	const [isLoggingIn, setIsLoggingIn] = useState(false)
	const onFinish = values => {
		users.map(x => {
			if (x.username === values.username && x.password === values.password){
				setMatch(true)
				Cookie.set('user', values);
				setUser(values)
				return 
			}
			else {
				setMatch(false)
			}
		})

		setIsLoggingIn(true)
  };

  const onFinishFailed = errorInfo => {
    message.error('An error ocurred.')
	};

	useEffect(() => {
		try {
			const u = JSON.parse(Cookie.get('user'))
			users.map(x => {
				if (x.username === u.username && x.password === u.password){
					setUser(u)
					setMatch(true)
					return 
				}
				else {
					setMatch(false)
				}
			})
		} catch(e) {
			
		}
	}, [isLoggingIn])
	
	return (
		<div>
			{user && user.username ? 
				<MainLayout user={user} />
				:
				<Card style={{width:400,margin:'0 auto', float: 'none', marginBottom:'10px', marginTop:50}}>
					<Form
						{...layout}
						name="basic"
						initialValues={{ remember: true }}
						onFinish={onFinish}
						onFinishFailed={onFinishFailed}
					>
						<Form.Item
							label="Username"
							name="username"
							rules={[{ required: true, message: 'Please input your username!' }]}
						>
							<Input />
						</Form.Item>
			
						<Form.Item
							label="Password"
							name="password"
							rules={[{ required: true, message: 'Please input your password!' }]}
						>
							<Input.Password />
						</Form.Item>
			
			
						<Form.Item {...tailLayout}>
							<Button type="primary" htmlType="submit" >
								Submit
							</Button>
						</Form.Item>
					</Form>
				</Card>
			}
		</div>
	)
}

ReactDOM.render(
	<App />,
  document.getElementById('root')
);