
import time
from collections import defaultdict
from math import sqrt
from itertools import combinations
from operator import itemgetter
import math

class Coord:
    def __init__(self,s):
        split = s.split(',')
        self.x= split[0]
        self.y =split[1]
        self.z=split[2]


def profiler(method):
    def wrapper_method(*arg, **kw):
        t = time.time()
        ret = method(*arg, **kw)
        print(
            "Method "
            + method.__name__
            + " took : "
            + "{:2.5f}".format(time.time() - t)
            + " sec"
        )
        return ret

    return wrapper_method


def distance(p1, p2):
    dx = p1[0] - p2[0]
    dy = p1[1] - p2[1]
    dz = p1[2] - p2[2]

    return math.floor(sqrt(dx * dx + dy * dy + dz * dz))
    return int(sqrt(dx * dx + dy * dy + dz * dz))


def distance_taxi(p1, p2):
    dx = p1[0] - p2[0]
    dy = p1[1] - p2[1]
    dz = p1[2] - p2[2]

    return abs(dx) + abs(dy) + abs(dz)


def get_common_pt_num(config_1, config_2):
    return max(
        [
            len(config_1[p0].intersection(config_2[p1]))
            for p0 in config_1
            for p1 in config_2
        ]
    )


def get_config(sensor_data):
    config = defaultdict(set)
    for p1 in sensor_data:
        for p2 in sensor_data:
            if p1 ==p2:
                continue
            config[p1].add(distance(p1, p2))
    #print(config)
    return config


def allign(config1, config2):
    #print("------")
    mapping = {}
    for p1 in config1:
        #print("p1: "+ str(p1))
        for p2 in config2:
            #print("p2: "+ str(p2))
            if len(config1[p1].intersection(config2[p2])) > 10:
                mapping[p1] = p2
    
    for key in mapping.keys():
        value = mapping[key]
        #print(str(key[0])+","+str(key[1])+","+str(key[2])+"-->"+str(value[0])+","+str(value[1])+","+str(value[2]))

    #print(len(mapping.keys()))
    cog_1_x = sum([k[0] for k in mapping.keys()]) / len(mapping.keys())
    #print(cog_1_x)

    cog_1_y = sum([k[1] for k in mapping.keys()]) / len(mapping.keys())
    #print(cog_1_y)
    cog_1_z = sum([k[2] for k in mapping.keys()]) / len(mapping.keys())
    #print(cog_1_z)

    cog_2_x = sum([k[0] for k in mapping.values()]) / len(mapping.values())
    #print(cog_2_x)
    cog_2_y = sum([k[1] for k in mapping.values()]) / len(mapping.values())
    #print(cog_2_y)
    cog_2_z = sum([k[2] for k in mapping.values()]) / len(mapping.values())
    #print(cog_2_z)

    p1 = list(mapping.keys())[0]
    p2 = mapping[p1]

    p1_mod = (round(p1[0] - cog_1_x), round(p1[1] - cog_1_y), round(p1[2] - cog_1_z))
    p2_mod = (round(p2[0] - cog_2_x), round(p2[1] - cog_2_y), round(p2[2] - cog_2_z))

    #print(p1)
    #print(p2)
    #print(p1_mod)
    #print(p2_mod)
    #print("------")
    rot = {}
    #print(list(map(abs, p2_mod)))
    for i in range(3):
        idx = list(map(abs, p2_mod)).index(abs(p1_mod[i]))
        #print(idx)
        #print(p1_mod[i] )
        #print( p2_mod[idx])
        #print(p1_mod[i] // p2_mod[idx])
        rot[i] = (idx, p1_mod[i] // p2_mod[idx])
        #print(rot[i])
    #print("rot")
    #print(rot)
    p2_rot = [0] * 3
    for i in range(3):
        p2_rot[i] = p2[rot[i][0]] * rot[i][1]
        #print(p2_rot[i])
    #print("p2 rot")
    #print(p2_rot)
    #print("p1")
    #print(p1)
    translation = []
    for i in range(3):
        translation.append(p2_rot[i] - p1[i])
    #print("trans")
    #print(translation)

    return rot, translation


def transform_points(rot, trans, points):
    
    new_points = set()

    for p in points:
        new = tuple(p[rot[i][0]] * rot[i][1] - trans[i] for i in range(3))
        #print(str(p) +"-->"+str(new))
        new_points.add(new)

    #print("-------------")
    return new_points


@profiler
def part1():
    #input = open("test19.txt").read().split("\n\n")
    input = open("input.txt").read().split("\n\n")
    
    scanners2 =defaultdict(set)
    for iScanner in range(len(input)):
        for i in range(1,len(input[iScanner])):
            scanners2[iScanner].add(input[iScanner][i])
    #print(scanners2)
    scanners = [
        tuple(map(lambda x: tuple(map(int, x.split(","))), s.split("\n")[1:]))
        for s in input
    ]
    #print(scanners)
    grid = set(scanners.pop(0))

    scanners_config = {s : get_config(s) for s in scanners}
    
    scanner_pos = []
    while len(scanners) > 0:
        #print(len(grid))
        grid_config = get_config(grid)
        scaners_common = [
            get_common_pt_num(grid_config, scanners_config[s]) for s in scanners
        ]
        print(scaners_common)
        s = scaners_common.index(max(scaners_common))
        
        
        #print(grid_config)
        rot, trams = allign(grid_config, scanners_config[scanners[s]])
        grid.update(transform_points(rot, trams, scanners[s]))

        print(len(grid))
        del scanners[s]
        scanner_pos.append(trams)

    sorted_grid = sorted(sorted(grid,key=lambda tup: tup[1]),key=lambda tup: tup[0])
    for g in sorted_grid:
        print(g)
    #print(sorted_grid)
    print(len(grid))
    return scanner_pos


@profiler
def part2(scanner_pos):
    print(max([distance_taxi(c[0], c[1]) for c in combinations(scanner_pos, 2)]))


scanner_pos = part1()
part2(scanner_pos)