import os
import sys

from clarifai.client import ClarifaiApi
import win32api, win32con
def click(x,y):
    win32api.SetCursorPos((x,y))
    win32api.mouse_event(win32con.MOUSEEVENTF_LEFTDOWN,x,y,0,0)
    win32api.mouse_event(win32con.MOUSEEVENTF_LEFTUP,x,y,0,0)

def main(argv):

	clarifai_api = ClarifaiApi() # assumes environment variables are set.
	result1 = clarifai_api.tag_images(open(r'TL.jpg', 'rb'))
	result2 = clarifai_api.tag_images(open(r'TM.jpg', 'rb'))
	result3 = clarifai_api.tag_images(open(r'TR.jpg', 'rb'))
	
	result4 = clarifai_api.tag_images(open(r'ML.jpg', 'rb'))
	result5 = clarifai_api.tag_images(open(r'MM.jpg', 'rb'))
	result6 = clarifai_api.tag_images(open(r'MR.jpg', 'rb'))
	
	result7 = clarifai_api.tag_images(open(r'LL.jpg', 'rb'))
	result8 = clarifai_api.tag_images(open(r'LM.jpg', 'rb'))
	result9 = clarifai_api.tag_images(open(r'LR.jpg', 'rb'))
	
	p1=result1['results'][0]['result']['tag']['classes']
	p2=result2['results'][0]['result']['tag']['classes']
	p3=result3['results'][0]['result']['tag']['classes']
	p4=result4['results'][0]['result']['tag']['classes']
	p5=result5['results'][0]['result']['tag']['classes']
	p6=result6['results'][0]['result']['tag']['classes']
	p7=result7['results'][0]['result']['tag']['classes']
	p8=result8['results'][0]['result']['tag']['classes']
	p9=result9['results'][0]['result']['tag']['classes']
	
	f = open("tag","r")
	myItem = f.read()
	p = open("pos","r")
	pos = p.read().split('|')
	
	x0 = int(pos[0].split(',')[0])
	y0 = int(pos[0].split(',')[1])
	
	x1 = int(pos[1].split(',')[0])
	y1 = int(pos[1].split(',')[1])
	
	x2 = int(pos[2].split(',')[0])
	y2 = int(pos[2].split(',')[1])
	
	x3 = int(pos[3].split(',')[0])
	y3 = int(pos[3].split(',')[1])
	
	x4 = int(pos[4].split(',')[0])
	y4 = int(pos[4].split(',')[1])
	
	x5 = int(pos[5].split(',')[0])
	y5 = int(pos[5].split(',')[1])
	
	x6 = int(pos[6].split(',')[0])
	y6 = int(pos[6].split(',')[1])
	
	x7 = int(pos[7].split(',')[0])
	y7 = int(pos[7].split(',')[1])
	
	x8 = int(pos[8].split(',')[0])
	y8 = int(pos[8].split(',')[1])
	
	if myItem in p1:
		click(x0,y0)
		print("TopLeft")
	if myItem in p2:
		click(x1,x1)
		print("TopMiddle")
	if myItem in p3:
		click(x2,x2)
		print("TopRight")
	if myItem in p4:
		click(x3,x3)
		print("MiddleLeft")
	if myItem in p5:
		click(x4,x4)
		print("MiddleMiddle")
	if myItem in p6:
		click(x5,x5)
		print("MiddleRight")
	if myItem in p7:
		click(x6,x6)
		print("LeftLeft")
	if myItem in p8:
		click(x7,y7)
		print("LeftMiddle")
	if myItem in p9:
		click(x8,y8)
		print("LeftRight")
	
	click(x8,y8+60)
	
if __name__ == '__main__':
	main(sys.argv)
