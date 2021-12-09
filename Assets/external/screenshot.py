
import pyautogui,time
x = 0
waittime = 60
time.sleep(10)
while True:
    x+=1
    screenshot = pyautogui.screenshot()
    screenshot.save(f"img{x}.png")
    time.sleep(60)
