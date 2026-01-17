import os
import shutil
from pathlib import Path

def move_mp4_files_to_parent(root_dir: str):
    """
    将 root_dir 下所有直接子目录中的 .mp4 文件剪切到 root_dir 根目录。
    
    参数:
        root_dir (str): 目标根目录路径，例如 r"E:\nice\b站  $ 荏苒s-_3546936430823643]"
    """
    root = Path(root_dir).resolve()
    
    if not root.exists():
        print(f"❌ 错误：目录不存在 → {root}")
        return
    
    if not root.is_dir():
        print(f"❌ 错误：指定路径不是目录 → {root}")
        return

    print(f"📁 正在处理目录：{root}")
    moved_count = 0

    # 遍历 root 下的所有直接子项（仅一级子目录）
    for item in root.iterdir():
        if item.is_dir():  # 只处理子目录
            print(f"  ➤ 检查子目录：{item.name}")
            # 查找该子目录中所有 .mp4 文件（不区分大小写）
            for mp4_file in item.glob("*.mp4"):
                if mp4_file.is_file():
                    target = root / mp4_file.name
                    # 如果目标已存在，避免覆盖（可选：加序号或跳过）
                    if target.exists():
                        print(f"    ⚠️ 跳过（目标已存在）：{mp4_file.name}")
                        continue
                    
                    try:
                        shutil.move(str(mp4_file), str(target))
                        print(f"    ✅ 剪切：{mp4_file.name}")
                        moved_count += 1
                    except Exception as e:
                        print(f"    ❌ 剪切失败：{mp4_file} → {e}")
            
            # 可选：剪切后如果子目录为空，可删除（此处不自动删，避免误操作）
            # if not any(item.iterdir()):
            #     item.rmdir()

    print(f"\n🎉 完成！共剪切 {moved_count} 个 .mp4 文件到 {root}")

# ======================
# 使用示例
# ======================
if __name__ == "__main__":
    # 👇 在这里修改为你自己的目录路径
    directory_A = r"E:\nice\b站\[荏苒s-_3546936430823643]"
    
    move_mp4_files_to_parent(directory_A)