from flask import Flask
from flask import request
import json
import random

app = Flask(__name__)

class Vector3():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

class Agent():
    def __init__(self, position, rotation):
        self.position = position
        self.rotation = rotation

class SimulationData():
    CITY_XZ_POSITION_MIN = -10
    CITY_XZ_POSITION_MAX = 10

    def __init__(self, count):
        self.agents = [Agent(self.get_random_position_in_city(), Vector3(0, 0, 0)) for i in range(count)]
    
    def count(self):
        return len(self.agents)

    def get_random_position_in_city(self):
        return Vector3(random.randint(self.CITY_XZ_POSITION_MIN, self.CITY_XZ_POSITION_MAX), 0, random.randint(self.CITY_XZ_POSITION_MIN, self.CITY_XZ_POSITION_MAX))

class SimpleEncoderClass(json.JSONEncoder):
    def default(self, obj):
        return obj.__dict__

simulation_data = SimulationData(0)

@app.route("/")
def hello_world():
    return "<p>Hello, World!</p>"

@app.get("/get-simulation-info")
def get_simulation_info():
    return "{ agent_count: " + str(simulation_data.count()) + " }"

@app.post("/init")
def init():
    request_data = request.get_json()
    agent_count = request_data['agent_count']

    global simulation_data
    simulation_data = SimulationData(agent_count)

    return ""

@app.get("/get-agent-count")
def get_agent_count():
    data = {}
    data['agent_count'] = simulation_data.count()
    return json.dumps(data)

@app.get("/get-agents-data")
def get_agents_data():
    return json.dumps(simulation_data, cls=SimpleEncoderClass)